using CleanArch.Application.Extensions;
using FluentValidation;

namespace CleanArch.Api.Features.LeaveAllocations.UpdateLeaveAllocations;

public static partial class UpdateLeaveAllocation
{
    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.Id)
                .NotNull()
                .WithError(LeaveAllocationErrors.IdIsRequired());

            RuleFor(m => m.NumberOfDays)
                .GreaterThan(0)
                .WithError(LeaveAllocationErrors.NumberOfDaysGreatherThan("{ComparisonValue}"));

            RuleFor(m => m.Period)
                .GreaterThanOrEqualTo(DateTime.Now.Year)
                .WithError(LeaveAllocationErrors.PeriodGreaterThanOrEqualToOngoingYear("{ComparisonValue}"));
        }
    }
}
