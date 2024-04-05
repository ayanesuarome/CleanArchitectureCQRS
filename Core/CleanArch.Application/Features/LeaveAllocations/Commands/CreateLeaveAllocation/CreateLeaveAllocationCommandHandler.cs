using CleanArch.Application.Abstractions.Identity;
using CleanArch.Application.ResultPattern;
using CleanArch.Application.Models.Identity;
using CleanArch.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using CleanArch.Application.Features.LeaveAllocations.Shared;
using CleanArch.Domain.Repositories;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Result<int>>
{
    private readonly ILeaveAllocationRepository _allocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IValidator<CreateLeaveAllocationCommand> _validator;
    private readonly IUserService _userService;

    public CreateLeaveAllocationCommandHandler(
        ILeaveAllocationRepository allocationRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IValidator<CreateLeaveAllocationCommand> validator,
        IUserService userService)
    {
        _allocationRepository = allocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _validator = validator;
        _userService = userService;
    }

    public async Task<Result<int>> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            return Result.Failure<int>(LeaveAllocationErrors.InvalidLeaveAllocation(validationResult.ToDictionary()));
        }

        // get leave types for allocations
        LeaveType leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

        // get employees
        List<Employee> employees = await _userService.GetEmployees();

        // get period
        int period = DateTime.Now.Year;

        // assign allocations if an allocation does not already exist for a period and leave type
        List<LeaveAllocation> allocations = [];

        foreach (Employee employee in employees)
        {
            bool allocationExist = await _allocationRepository.AllocationExists(employee.Id, leaveType.Id, period);

            if(!allocationExist)
            {
                LeaveAllocation allocation = new()
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveType.Id,
                    Period = period
                };
                
                allocation.UpdateNumberOfDays(leaveType.DefaultDays);
                
                allocations.Add(allocation);
            }
        }

        int rowsAffected = 0;

        if(allocations.Any())
        {
            rowsAffected = await _allocationRepository.CreateListAsync(allocations);
        }

        return Result.Success<int>(rowsAffected);
    }
}
