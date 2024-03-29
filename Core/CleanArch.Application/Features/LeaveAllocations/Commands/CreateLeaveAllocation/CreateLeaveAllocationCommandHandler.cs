﻿using CleanArch.Application.Exceptions;
using CleanArch.Application.Interfaces.Identity;
using CleanArch.Application.Models.Identity;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
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

    public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            throw new BadRequestException($"Invalid {nameof(LeaveAllocation)}", validationResult);
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

        return rowsAffected;
    }
}
