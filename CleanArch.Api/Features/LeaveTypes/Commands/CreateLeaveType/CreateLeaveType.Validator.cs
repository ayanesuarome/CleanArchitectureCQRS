using CleanArch.Domain.Repositories;
using FluentValidation;

namespace CleanArch.Api.Features.LeaveTypes;

public static partial class CreateLeaveType
{
    public class Validator : AbstractValidator<Command>
    {
        private readonly ILeaveTypeRepository _repository;

        public Validator(ILeaveTypeRepository repository)
        {
            RuleFor(m => m.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage("{PropertyName} is required")
                .MaximumLength(70)
                    .WithMessage("{PropertyName} must be up to {MaxLength} characters")
                .MustAsync(LeaveTypeUniqueName)
                    .WithMessage("Leave type already exist");

            RuleFor(m => m.DefaultDays)
                .InclusiveBetween(1, 100)
                    .WithMessage("{PropertyName} must be between {From} - {To}");

            _repository = repository;
        }

        private async Task<bool> LeaveTypeUniqueName(string name, CancellationToken token)
        {
            return await _repository.IsUniqueAsync(name, token);
        }
    }
}
