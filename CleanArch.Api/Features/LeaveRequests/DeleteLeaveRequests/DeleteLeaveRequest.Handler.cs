using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.DeleteLeaveRequests;

public static partial class DeleteLeaveRequest
{
    internal sealed class Handler(ILeaveRequestRepository repository)
        : IRequestHandler<Command, Result>
    {
        private readonly ILeaveRequestRepository _repository = repository;

        public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
        {
            LeaveRequest leaveRequest = await _repository.GetByIdAsync(command.Id);

            if (leaveRequest is null)
            {
                return new NotFoundResult(DomainErrors.LeaveRequest.NotFound(command.Id));
            }

            _repository.Delete(leaveRequest);

            return new SuccessResult();
        }
    }
}
