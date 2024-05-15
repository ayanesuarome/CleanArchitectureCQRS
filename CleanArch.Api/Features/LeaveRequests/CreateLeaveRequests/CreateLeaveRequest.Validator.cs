using CleanArch.Application.Extensions;
using FluentValidation;

namespace CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;

public static partial class CreateLeaveRequest
{
    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.LeaveTypeId)
                .NotEmpty()
                .WithError(ValidationErrors.CreateLeaveRequest.LeaveTypeIdIsRequired);

            RuleFor(m => m.StartDate)
                .NotEmpty()
                .WithError(ValidationErrors.CreateLeaveRequest.StartDateIsRequired);

            RuleFor(m => m.EndDate)
                .NotEmpty()
                .WithError(ValidationErrors.CreateLeaveRequest.EndDateIsRequired);
        }
    }
}
