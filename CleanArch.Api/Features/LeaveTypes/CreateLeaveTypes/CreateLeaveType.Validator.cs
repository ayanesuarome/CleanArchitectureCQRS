using CleanArch.Application.Extensions;
using CleanArch.Domain.Repositories;
using FluentValidation;

namespace CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;

public static partial class CreateLeaveType
{
    public sealed class Validator : AbstractValidator<Command>
    {
        private readonly ILeaveTypeRepository _repository;

        public Validator(ILeaveTypeRepository repository)
        {
            RuleFor(m => m.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithError(LeaveTypeErrors.NameRequired())
                .MaximumLength(70)
                    .WithError(LeaveTypeErrors.NameMaximumLength("{MaxLength}"))
                .MustAsync(LeaveTypeUniqueName)
                    .WithError(LeaveTypeErrors.NameIsUnique());

            RuleFor(m => m.DefaultDays)
                .InclusiveBetween(1, 100)
                    .WithError(LeaveTypeErrors.DefaultDaysRange("{From} - {To}"));

            _repository = repository;
        }

        private async Task<bool> LeaveTypeUniqueName(string name, CancellationToken token)
        {
            return await _repository.IsUniqueAsync(name, token);
        }
    }
}
