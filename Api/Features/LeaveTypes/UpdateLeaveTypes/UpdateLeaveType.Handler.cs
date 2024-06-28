using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Core.ValueObjects;
using CleanArch.Domain.Errors;
using CleanArch.Domain.LeaveTypes;
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
            LeaveType leaveType = await _repository.GetByIdAsync(new LeaveTypeId(command.Id));

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

            LeaveTypeNameUniqueRequirement requirement = new(async () =>
                await _repository.IsUniqueAsync(nameResult.Value, cancellationToken));

            Result updateNameResult = leaveType.UpdateName(nameResult.Value, requirement);

            if(updateNameResult.IsFailure)
            {
                return Result.Failure<Unit>(updateNameResult.Error);
            }

            leaveType.UpdateDefaultDays(defaultDaysResult.Value);

            leaveType.NotifyUpdate();

            return Result.Success<Unit>(Unit.Value);
        }
    }
}
