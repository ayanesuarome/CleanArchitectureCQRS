﻿using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation;

namespace CleanArch.Application.Features.LeaveTypes.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _repository;

    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository repository)
    {
        _repository = repository;

        RuleFor(m => m.Id)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");

        RuleFor(m => m.Name)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(70)
                .WithMessage("{PropertyName} must be up to 70 characters")
            .MustAsync(LeaveTypeUniqueName)
                .WithMessage("Leave type already exist");

        RuleFor(m => m.DefaultDays)
            .InclusiveBetween(1, 100)
                .WithMessage("{PropertyName} must be between 1 - 100");
    }

    private async Task<bool> LeaveTypeUniqueName(string name, CancellationToken cancellation)
    {
        if(!string.IsNullOrWhiteSpace(name))
        {
            return await _repository.IsUniqueAsync(name, cancellation);
        }

        // No name is provided to update
        return true;
    }
}
