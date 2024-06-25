using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Core.ValueObjects;
using CleanArch.Domain.LeaveTypes;

namespace CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;

public static partial class CreateLeaveType
{
    internal sealed class Handler : ICommandHandler<Command, Result<Guid>>
    {
        private readonly ILeaveTypeRepository _repository;

        public Handler(ILeaveTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Guid>> Handle(Command command, CancellationToken cancellationToken)
        {
            Result<Name> nameResult = Name.Create(command.Name);
            Result<DefaultDays> defaultDaysResult = DefaultDays.Create(command.DefaultDays);

            Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(nameResult, defaultDaysResult);

            if (firstFailureOrSuccess.IsFailure)
            {
                return Result.Failure<Guid>(firstFailureOrSuccess.Error);
            }

            LeaveTypeNameUniqueRequirement requirement = new(async () =>
                await _repository.IsUniqueAsync(nameResult.Value, cancellationToken));

            Result<LeaveType> leaveTypeResult = LeaveType.Create(nameResult.Value, defaultDaysResult.Value, requirement);
            
            if(leaveTypeResult.IsFailure)
            {
                return Result.Failure<Guid>(leaveTypeResult.Error);
            }

            _repository.Add(leaveTypeResult.Value);

            return Result.Success<Guid>(leaveTypeResult.Value.Id);
        }
    }
}
