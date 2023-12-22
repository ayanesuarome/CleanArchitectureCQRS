﻿using CleanArch.Application.Features.LeaveAllocations.Shared;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandValidator : AbstractValidator<CreateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _repository;

    public CreateLeaveAllocationCommandValidator(ILeaveTypeRepository repository)
    {
        _repository = repository;

        RuleFor(m => m.LeaveTypeId)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} does not exist");

        Include(new BaseLeaveAllocationCommandValidtor());
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        // TODO: measure with benchmark whether getting the entity or using bool has better performance
        LeaveType leaveType = await _repository.GetByIdAsync(id);
        return leaveType != null;
    }
}