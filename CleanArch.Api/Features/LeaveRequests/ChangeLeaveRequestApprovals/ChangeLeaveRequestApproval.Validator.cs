using CleanArch.Application.Extensions;
using FluentValidation;

namespace CleanArch.Api.Features.LeaveRequests.ChangeLeaveRequestApprovals;

public static partial class ChangeLeaveRequestApproval
{
    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.Id)
                .NotEmpty()
                .WithError(ValidationErrors.ChangeLeaveRequestApproval.IdIsRequired);
        }
    }
}
