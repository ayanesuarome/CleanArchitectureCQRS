using CleanArch.Application.Abstractions.Identity;
using CleanArch.Contracts.Identity;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Api.Features.LeaveAllocations.CreateLeaveAllocations;

public static partial class CreateLeaveAllocation
{
    public sealed class Handler : IRequestHandler<Command, Result<int>>
    {
        private readonly ILeaveAllocationRepository _allocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IValidator<Command> _validator;
        private readonly IUserService _userService;

        public Handler(
            ILeaveAllocationRepository allocationRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IValidator<Command> validator,
            IUserService userService)
        {
            _allocationRepository = allocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _validator = validator;
            _userService = userService;
        }

        public async Task<Result<int>> Handle(Command command, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Result.Failure<int>(ValidationErrors.CreateLeaveAllocation.CreateLeaveAllocationValidation(validationResult.ToString()));
            }

            // get leave types for allocations
            LeaveType leaveType = await _leaveTypeRepository.GetByIdAsync(command.LeaveTypeId);

            if(leaveType is null)
            {
                return new FailureResult<int>(DomainErrors.LeaveAllocation.LeaveTypeMustExist);
            }

            // get employees
            IEnumerable<Employee> employees = await _userService.GetEmployees();

            // get period
            int period = DateTime.Now.Year;

            // assign allocations if an allocation does not already exist for a period and leave type
            List<LeaveAllocation> allocations = [];

            foreach (Employee employee in employees)
            {
                bool allocationExist = await _allocationRepository.AllocationExists(employee.Id, leaveType.Id, period);

                if (!allocationExist)
                {
                    LeaveAllocation allocation = new(period, leaveType, employee.Id);
                    allocation.ChangeNumberOfDays(leaveType.DefaultDays.Value);
                    allocations.Add(allocation);
                }
            }

            int rowsAffected = 0;

            if (allocations.Any())
            {
                rowsAffected = await _allocationRepository.CreateListAsync(allocations);
            }

            return Result.Success<int>(rowsAffected);
        }
    }
}
