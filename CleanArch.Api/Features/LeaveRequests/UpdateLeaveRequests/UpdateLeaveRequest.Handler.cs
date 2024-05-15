using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using CleanArch.Domain.Errors;
using CleanArch.Domain.ValueObjects;
using CleanArch.Application.Abstractions.Messaging;

namespace CleanArch.Api.Features.LeaveRequests.UpdateLeaveRequests;

public static partial class UpdateLeaveRequest
{
    internal sealed class Handler : ICommandHandler<Command, LeaveRequest>
    {
        private readonly ILeaveRequestRepository _repository;

        public Handler(ILeaveRequestRepository repository) => _repository = repository;

        public async Task<Result<LeaveRequest>> Handle(Command command, CancellationToken cancellationToken)
        {
            LeaveRequest leaveRequest = await _repository.GetByIdAsync(command.Id);

            if (leaveRequest is null)
            {
                return new NotFoundResult<LeaveRequest>(DomainErrors.LeaveRequest.NotFound(command.Id));
            }

            Result<DateRange> rangeResult = DateRange.Create(command.StartDate, command.EndDate);
            Result<Comment>? commentResult = null;

            if (command.Comments is not null)
            {
                commentResult = Comment.Create(command.Comments);
            }

            Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(rangeResult, commentResult);

            if (firstFailureOrSuccess.IsFailure)
            {
                return new FailureResult<LeaveRequest>(firstFailureOrSuccess.Error);
            }

            leaveRequest.UpdateDateRange(rangeResult.Value);
            leaveRequest.UpdateComments(commentResult.Value);

            // can be omitted since EF keeps track of entity changes
            _repository.Update(leaveRequest);

            return new SuccessResult<LeaveRequest>(leaveRequest);
        }
    }
}
