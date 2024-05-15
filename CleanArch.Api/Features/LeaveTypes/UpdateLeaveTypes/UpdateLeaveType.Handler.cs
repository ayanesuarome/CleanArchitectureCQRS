using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using CleanArch.Domain.ValueObjects;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;

public static partial class UpdateLeaveType
{
    internal sealed class Handler : ICommandHandler<Command, Result<Unit>>
    {
        private readonly ILeaveTypeRepository _repository;

        public Handler(ILeaveTypeRepository repository) => _repository = repository;

        public async Task<Result<Unit>> Handle(Command command, CancellationToken cancellationToken)
        {
            LeaveType leaveType = await _repository.GetByIdAsync(command.Id);

            if (leaveType is null)
            {
                return new NotFoundResult<Unit>(DomainErrors.LeaveType.NotFound(command.Id));
            }

            Result<Name> nameResult = Name.Create(command.Name);
            Result<DefaultDays> defaultDaysResult = DefaultDays.Create(command.DefaultDays);

            Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(nameResult, defaultDaysResult);

            if(firstFailureOrSuccess.IsFailure)
            {
                return Result.Failure<Unit>(firstFailureOrSuccess.Error);
            }

            Result updateNameResult = await leaveType.UpdateName(nameResult.Value, _repository);

            if(updateNameResult.IsFailure)
            {
                return Result.Failure<Unit>(updateNameResult.Error);
            }

            leaveType.UpdateDefaultDays(defaultDaysResult.Value);

            return Result.Success<Unit>(Unit.Value);
        }
    }
}
