using FluentValidation;

namespace CleanArch.Application.Features.LeaveRequests.Shared;

public class BaseLeaveRequestCommandValidator : AbstractValidator<BaseLeaveRequestCommand>
{
    public BaseLeaveRequestCommandValidator()
    {
        RuleFor(m => m.RequestingEmployeeId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");

        RuleFor(m => m.StartDate)
            .LessThan(m => m.EndDate)
            .WithMessage("{PropertyName} must be before {ComparisonValue}");

        RuleFor(m => m.EndDate)
            .GreaterThan(m => m.StartDate)
            .WithMessage("{PropertyName} must be after {ComparisonValue}");
    }
}
