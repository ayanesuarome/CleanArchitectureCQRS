using CleanArch.Application.Abstractions.Data;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using CleanArch.Domain.Requirements;
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

            LeaveTypeNameUniqueRequirement requirement = new (async () =>
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
