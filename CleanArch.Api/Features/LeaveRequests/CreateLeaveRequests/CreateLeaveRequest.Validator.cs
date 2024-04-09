using CleanArch.Application.Extensions;
using CleanArch.Domain.Repositories;
using FluentValidation;

namespace CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;

public static partial class CreateLeaveRequest
{
    public sealed class Validator : AbstractValidator<Command>
    {
        private readonly ILeaveTypeRepository _repository;

        public Validator(ILeaveTypeRepository repository)
        {
            _repository = repository;

            RuleFor(m => m.LeaveTypeId)
                .MustAsync(LeaveTypeMustExist)
                .WithError(LeaveRequestErrors.LeaveTypeMustExist());

            RuleFor(m => m.RequestComments)
                .MaximumLength(300)
                .WithError(LeaveRequestErrors.RequestCommentsMaximumLength(300));

            Include(new BaseCommandValidator());
        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
        {
            return await _repository.AnyAsync(id, token);
        }
    }
}
