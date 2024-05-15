using CleanArch.Application.Abstractions.Data;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using CleanArch.Domain.ValueObjects;

namespace CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;

public static partial class CreateLeaveType
{
    internal sealed class Handler : ICommandHandler<Command, Result<Guid>>
    {
        private readonly ILeaveTypeRepository _repository;
        private readonly IUnitOfWork unitOfWork;

        public Handler(ILeaveTypeRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            this.unitOfWork = unitOfWork;
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

            if (!await _repository.IsUniqueAsync(nameResult.Value, cancellationToken))
            {
                return Result.Failure<Guid>(DomainErrors.LeaveType.DuplicateName);
            }

            LeaveType leaveType = new(nameResult.Value, defaultDaysResult.Value);
            _repository.Add(leaveType);

            return Result.Success<Guid>(leaveType.Id);
        }
    }
}
