using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.DeleteLeaveTypes;

public static partial class DeleteLeaveType
{
    internal sealed class Handler(ILeaveTypeRepository repository)
        : IRequestHandler<Command, Result<Unit>>
    {
        private readonly ILeaveTypeRepository _repository = repository;

        public async Task<Result<Unit>> Handle(Command command, CancellationToken cancellationToken)
        {
            LeaveType leaveTypeToDelete = await _repository.GetByIdAsync(command.Id);

            if (leaveTypeToDelete is null)
            {
                return new NotFoundResult<Unit>(DomainErrors.LeaveType.NotFound(command.Id));
            }

            _repository.Delete(leaveTypeToDelete);
            return new SuccessResult<Unit>(Unit.Value);
        }
    }

}
