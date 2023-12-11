using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation;

namespace CleanArch.Application.Features.LeaveTypes.Commands.UpdateLeaveTypes;

public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _repository;

    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository repository)
    {
        RuleFor(m => m.Name)
            .MaximumLength(70)
                .WithMessage("{PropertyName} must be up to 70 characters")
            .MustAsync(LeaveTypeUniqueName)
                .WithMessage("Leave type already exist");

        RuleFor(m => m.DefaultDays)
            .InclusiveBetween(1, 100)
                .WithMessage("{PropertyName} must be between 1 - 100");

        _repository = repository;
    }

    private async Task<bool> LeaveTypeUniqueName(string name, CancellationToken token)
    {
        if(!string.IsNullOrWhiteSpace(name))
        {
            return await _repository.IsUniqueAsync(name, token);
        }

        // No name is provided to update
        return true;
    }
}
