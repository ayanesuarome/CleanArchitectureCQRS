using CleanArch.Application.Extensions;
using CleanArch.Domain.Repositories;
using FluentValidation;

namespace CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;

public static partial class CreateLeaveType
{
    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithError(ValidationErrors.CreateLeaveType.NameIsRequired);

            RuleFor(m => m.DefaultDays)
                .NotEmpty()
                .WithError(ValidationErrors.CreateLeaveType.DefaultDaysIsRequired);
        }
    }
}
