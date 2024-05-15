using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using CleanArch.Domain.ValueObjects;

namespace CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;

public static partial class CreateLeaveType
{
    internal sealed class Handler : ICommandHandler<Command, Guid>
    {
        private readonly ILeaveTypeRepository _repository;

        public Handler(ILeaveTypeRepository repository) => _repository = repository;

        public async Task<Result<Guid>> Handle(Command command, CancellationToken cancellationToken)
        {
            Result<Name> nameResult = Name.Create(command.Name);
            Result<DefaultDays> defaultDaysResult = DefaultDays.Create(command.DefaultDays);

            Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(nameResult, defaultDaysResult);

            if (firstFailureOrSuccess.IsFailure)
            {
                return new FailureResult<Guid>(firstFailureOrSuccess.Error);
            }

            if (!await _repository.IsUniqueAsync(nameResult.Value, cancellationToken))
            {
                return new FailureResult<Guid>(DomainErrors.LeaveType.DuplicateName);
            }

            LeaveType leaveTypeToCreate = new(nameResult.Value, defaultDaysResult.Value);

            Result<Name> nameResult1 = Name.Create("Test");
            Result<DefaultDays> defaultDaysResult1 = DefaultDays.Create(15);

            _repository.Add(leaveTypeToCreate);

            return new SuccessResult<Guid>(leaveTypeToCreate.Id);
        }
    }
}
