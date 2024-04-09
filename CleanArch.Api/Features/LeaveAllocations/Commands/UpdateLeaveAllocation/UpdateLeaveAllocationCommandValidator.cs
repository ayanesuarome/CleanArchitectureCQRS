using FluentValidation;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
{
    public UpdateLeaveAllocationCommandValidator()
    {
        RuleFor(m => m.Id)
            .NotNull()
            .WithMessage("{PropertyName} is required");

        RuleFor(m => m.NumberOfDays)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greather than {ComparisonValue}");

        RuleFor(m => m.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year)
            .WithMessage("{PropertyName} must be after {ComparisonValue}");
    }
}
