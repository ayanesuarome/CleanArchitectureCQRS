using CleanArch.Application.Features.LeaveRequests.Shared;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation;

namespace CleanArch.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandValidator : AbstractValidator<CreateLeaveRequestCommand>
{
    private readonly ILeaveTypeRepository _repository;

    public CreateLeaveRequestCommandValidator(ILeaveTypeRepository repository)
    {
        _repository = repository;

        RuleFor(m => m.LeaveTypeId)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .WithMessage("{PropertyName} should be greather than 0")
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} does not exist");

        Include(new BaseLeaveRequestCommandValidator());
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        // TODO: measure with benchmark whether getting the entity or using bool has better performance
        LeaveType leaveType = await _repository.GetByIdAsync(id);
        return leaveType != null;
    }
}
