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
                .WithError(ValidationErrors.UpdateLeaveAllocation.IdIsRequired);

            RuleFor(m => m.NumberOfDays)
                .GreaterThan(0)
                .WithError(ValidationErrors.UpdateLeaveAllocation.NumberOfDaysGreatherThan("{ComparisonValue}"));

            RuleFor(m => m.Period)
                .GreaterThanOrEqualTo(DateTime.Now.Year)
                .WithError(ValidationErrors.UpdateLeaveAllocation.PeriodGreaterThanOrEqualToOngoingYear("{ComparisonValue}"));
        }
    }
}
