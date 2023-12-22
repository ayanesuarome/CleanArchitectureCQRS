using FluentValidation;

namespace CleanArch.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandValidator : AbstractValidator<ChangeLeaveRequestApprovalCommand>
{
    public ChangeLeaveRequestApprovalCommandValidator()
    {
        RuleFor(m => m.Approved)
            .NotNull()
            .WithMessage("Approval status cannot be null");
    }
}
