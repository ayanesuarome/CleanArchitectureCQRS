using CleanArch.Application.Extensions;
using CleanArch.Domain.Errors;
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
                    .WithError(ValidationErrors.CreateLeaveType.NameIsRequired)
                .MaximumLength(70)
                    .WithError(ValidationErrors.CreateLeaveType.NameMaximumLength("{MaxLength}"))
                .MustAsync(LeaveTypeUniqueName)
                    .WithError(DomainErrors.LeaveType.DuplicateName);

            RuleFor(m => m.DefaultDays)
                .InclusiveBetween(1, 100)
                    .WithError(ValidationErrors.UpdateLeaveType.DefaultDaysRange("{From} - {To}"));

            _repository = repository;
        }

        private async Task<bool> LeaveTypeUniqueName(string name, CancellationToken token)
        {
            return await _repository.IsUniqueAsync(name, token);
        }
    }
}
