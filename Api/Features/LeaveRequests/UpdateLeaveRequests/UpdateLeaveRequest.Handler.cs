﻿using CleanArch.Domain.Errors;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.LeaveRequests;

namespace CleanArch.Api.Features.LeaveRequests.UpdateLeaveRequests;

public static partial class UpdateLeaveRequest
{
    internal sealed class Handler : ICommandHandler<Command, Result<LeaveRequest>>
    {
        private readonly ILeaveRequestRepository _repository;

        public Handler(ILeaveRequestRepository repository) => _repository = repository;

        public async Task<Result<LeaveRequest>> Handle(Command command, CancellationToken cancellationToken)
        {
            LeaveRequest leaveRequest = await _repository.GetByIdAsync(new LeaveRequestId(command.Id));

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
                return Result.Failure<LeaveRequest>(firstFailureOrSuccess.Error);
            }

            leaveRequest.UpdateDateRange(rangeResult.Value);
            leaveRequest.UpdateComments(commentResult.Value);

            // can be omitted since EF keeps track of entity changes
            _repository.Update(leaveRequest);

            return Result.Success<LeaveRequest>(leaveRequest);
        }
    }
}
