using CleanArch.Application.Features.LeaveRequests.Shared;
using FluentValidation;

namespace CleanArch.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandValidator : AbstractValidator<UpdateLeaveRequestCommand>
{
    public UpdateLeaveRequestCommandValidator()
    {
        RuleFor(m => m.Id)
            .NotNull()
            .WithMessage("{PropertyName} is required");

        Include(new BaseLeaveRequestCommandValidator());
    }
}
