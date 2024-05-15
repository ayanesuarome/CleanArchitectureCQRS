using CleanArch.Application.Extensions;
using FluentValidation;

namespace CleanArch.Api.Features.LeaveAllocations.CreateLeaveAllocations;

public static partial class CreateLeaveAllocation
{
    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.LeaveTypeId)
                .NotEmpty()
                .WithError(ValidationErrors.CreateLeaveAllocation.LeaveTypeIdIsRequired);
        }
    }
}
