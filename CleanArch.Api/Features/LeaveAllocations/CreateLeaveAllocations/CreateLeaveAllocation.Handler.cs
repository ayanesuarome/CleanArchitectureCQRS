using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.Identity;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using CleanArch.Domain.ValueObjects;

namespace CleanArch.Api.Features.LeaveAllocations.CreateLeaveAllocations;

public static partial class CreateLeaveAllocation
{
    internal sealed class Handler : ICommandHandler<Command, Result<int>>
    {
        private readonly ILeaveAllocationRepository _allocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUserService _userService;

        public Handler(
            ILeaveAllocationRepository allocationRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IUserService userService)
        {
            _allocationRepository = allocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _userService = userService;
        }

        public async Task<Result<int>> Handle(Command command, CancellationToken cancellationToken)
        {
            // get leave types for allocations
            LeaveType leaveType = await _leaveTypeRepository.GetByIdAsync(new LeaveTypeId(command.LeaveTypeId));

            if(leaveType is null)
            {
                return Result.Failure<int>(DomainErrors.LeaveAllocation.LeaveTypeMustExist);
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

            if (allocations.Any())
            {
                _allocationRepository.AddRange(allocations);
            }

            return Result.Success<int>(allocations.Count);
        }
    }
}
