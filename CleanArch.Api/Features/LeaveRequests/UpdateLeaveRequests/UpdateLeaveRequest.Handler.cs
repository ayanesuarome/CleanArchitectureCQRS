using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using CleanArch.Domain.Errors;
using CleanArch.Domain.ValueObjects;

namespace CleanArch.Api.Features.LeaveRequests.UpdateLeaveRequests;

public static partial class UpdateLeaveRequest
{
    internal sealed class Handler : IRequestHandler<Command, Result<LeaveRequest>>
    {
        private readonly ILeaveRequestRepository _repository;
        private readonly IValidator<Command> _validator;

        public Handler(
            ILeaveRequestRepository repository,
            IValidator<Command> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<LeaveRequest>> Handle(Command command, CancellationToken cancellationToken)
        {
            LeaveRequest leaveRequest = await _repository.GetByIdAsync(command.Id);

            if (leaveRequest is null)
            {
                return new NotFoundResult<LeaveRequest>(DomainErrors.LeaveRequest.NotFound(command.Id));
            }

            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new FailureResult<LeaveRequest>(
                    ValidationErrors.UpdateLeaveRequest.UpdateLeaveRequestValidation(validationResult.ToString()));
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

            _repository.Update(leaveRequest);

            return new SuccessResult<LeaveRequest>(leaveRequest);
        }
    }
}
