using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Errors;
using CleanArch.Domain.LeaveRequests;

namespace CleanArch.Api.Features.LeaveRequests.DeleteLeaveRequests;

public static partial class DeleteLeaveRequest
{
    internal sealed class Handler(ILeaveRequestRepository repository) : ICommandHandler<Command, Result>
    {
        private readonly ILeaveRequestRepository _repository = repository;

        public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
        {
            LeaveRequest leaveRequest = await _repository.GetByIdAsync(new LeaveRequestId(command.Id));

            if (leaveRequest is null)
            {
                return new NotFoundResult(DomainErrors.LeaveRequest.NotFound(command.Id));
            }

            _repository.Delete(leaveRequest);

            return Result.Success();
        }
    }
}
