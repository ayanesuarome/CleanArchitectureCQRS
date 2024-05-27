using CleanArch.Application.Extensions;
using FluentValidation;

namespace CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;

public static partial class UpdateLeaveType
{
    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.Id)
                .NotEmpty()
                .WithError(ValidationErrors.UpdateLeaveType.IdIsRequired);

            RuleFor(m => m.Name)
                .NotEmpty()
                .WithError(ValidationErrors.UpdateLeaveType.NameIsRequired);

            RuleFor(m => m.DefaultDays)
                .NotEmpty()
                .WithError(ValidationErrors.UpdateLeaveType.DefaultDaysIsRequired);
        }
    }
}
