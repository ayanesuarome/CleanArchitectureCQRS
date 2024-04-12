using CleanArch.Application.Extensions;
using FluentValidation;

namespace CleanArch.Api.Features.LeaveAllocations;

public sealed class BaseCommandValidtor : AbstractValidator<BaseCommnad>
{
    public BaseCommandValidtor()
    {
        RuleFor(m => m.NumberOfDays)
            .GreaterThan(0)
            .WithError(LeaveAllocationErrors.NumberOfDaysGreatherThan("{PropertyName} must be greather than {ComparisonValue}"));

        RuleFor(m => m.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year)
            .WithError(LeaveAllocationErrors.PeriodGreaterThanOrEqualToOngoingYear("{PropertyName} must be after {ComparisonValue}"));
    }
}
