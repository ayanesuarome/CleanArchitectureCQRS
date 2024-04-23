using CleanArch.Application.Extensions;
using FluentValidation;

namespace CleanArch.Api.Features.LeaveRequests.UpdateLeaveRequests;

public static partial class UpdateLeaveRequest
{
    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.Id)
                .NotEmpty()
                .WithError(ValidationErrors.UpdateLeaveRequest.IdIsRequired);

            RuleFor(m => m.StartDate)
                .NotEmpty()
                .WithError(ValidationErrors.UpdateLeaveRequest.StartDateIsRequired);

            RuleFor(m => m.EndDate)
                .NotEmpty()
                .WithError(ValidationErrors.UpdateLeaveRequest.EndDateIsRequired);
        }
    }
}
