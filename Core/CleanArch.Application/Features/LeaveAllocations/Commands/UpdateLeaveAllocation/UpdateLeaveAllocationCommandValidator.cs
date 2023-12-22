using CleanArch.Application.Features.LeaveAllocations.Shared;
using FluentValidation;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
{
    public UpdateLeaveAllocationCommandValidator()
    {
        RuleFor(m => m.Id)
            .NotNull()
            .WithMessage("{PropertyName} is required");

        Include(new BaseLeaveAllocationCommandValidtor());
    }
}
