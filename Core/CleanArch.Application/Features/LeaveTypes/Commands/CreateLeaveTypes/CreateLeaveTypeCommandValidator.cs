using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation;

namespace CleanArch.Application.Features.LeaveTypes.Commands.CreateLeaveTypes;

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _repository;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository repository)
    {
        RuleFor(m => m.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("{PropertyName} is required")
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
        return await _repository.IsUniqueAsync(name, token);
    }
}
