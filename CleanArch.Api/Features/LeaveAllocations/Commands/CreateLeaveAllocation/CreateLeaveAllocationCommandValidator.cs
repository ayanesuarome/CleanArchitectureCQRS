using CleanArch.Domain.Repositories;
using FluentValidation;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandValidator : AbstractValidator<CreateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _repository;

    public CreateLeaveAllocationCommandValidator(ILeaveTypeRepository repository)
    {
        _repository = repository;

        RuleFor(m => m.LeaveTypeId)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} does not exist.");
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        return await _repository.AnyAsync(id, token);
    }
}
