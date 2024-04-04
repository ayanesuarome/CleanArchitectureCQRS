using CleanArch.Application.Features.LeaveRequests.Shared;
using CleanArch.Domain.Repositories;
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

        RuleFor(m => m.RequestComments)
            .MaximumLength(300)
            .WithMessage("{PropertyName} must be up to {ComparisonValue}");

        Include(new BaseLeaveRequestCommandValidator());
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        return await _repository.AnyAsync(id, token);
    }
}
