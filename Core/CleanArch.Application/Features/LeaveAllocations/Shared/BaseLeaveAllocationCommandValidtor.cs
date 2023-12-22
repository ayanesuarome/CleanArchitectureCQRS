using FluentValidation;

namespace CleanArch.Application.Features.LeaveAllocations.Shared;

public class BaseLeaveAllocationCommandValidtor : AbstractValidator<BaseLeaveAllocationCommnad>
{
    public BaseLeaveAllocationCommandValidtor()
    {
        RuleFor(m => m.NumberOfDays)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greather than {ComparisonValue}");

        RuleFor(m => m.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year)
            .WithMessage("{PropertyName} must be after {ComparisonValue}");
    }
}
