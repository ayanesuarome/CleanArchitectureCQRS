using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Errors;
using CleanArch.Domain.LeaveTypes;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.DeleteLeaveTypes;

public static partial class DeleteLeaveType
{
    internal sealed class Handler(ILeaveTypeRepository repository) : ICommandHandler<Command, Result<Unit>>
    {
        private readonly ILeaveTypeRepository _repository = repository;

        public async Task<Result<Unit>> Handle(Command command, CancellationToken cancellationToken)
        {
            LeaveType leaveTypeToDelete = await _repository.GetByIdAsync(new LeaveTypeId(command.Id));

            if (leaveTypeToDelete is null)
            {
                return new NotFoundResult<Unit>(DomainErrors.LeaveType.NotFound(command.Id));
            }

            _repository.Delete(leaveTypeToDelete);
            return Result.Success<Unit>(Unit.Value);
        }
    }

}
