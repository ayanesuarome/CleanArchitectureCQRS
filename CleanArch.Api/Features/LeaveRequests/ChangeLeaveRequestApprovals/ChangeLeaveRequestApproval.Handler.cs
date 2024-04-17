using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.ChangeLeaveRequestApprovals;

public static partial class ChangeLeaveRequestApproval
{
    internal sealed class Handler : IRequestHandler<Command, Result<LeaveRequest>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IValidator<Command> _validator;

        public Handler(
            ILeaveRequestRepository leaveRequestRepository,
            ILeaveAllocationRepository leaveAllocationRepository,
            IValidator<Command> validator)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveAllocationRepository = leaveAllocationRepository;
            _validator = validator;
        }

        public async Task<Result<LeaveRequest>> Handle(Command command, CancellationToken cancellationToken)
        {
            LeaveRequest leaveRequest = await _leaveRequestRepository.GetByIdAsync(command.Id);

            if (leaveRequest is null)
            {
                return new NotFoundResult<LeaveRequest>(DomainErrors.LeaveRequest.NotFound(command.Id));
            }

            ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new FailureResult<LeaveRequest>(
                    ValidationErrors.ChangeLeaveRequestApproval.ChangeLeaveRequestApprovalValidation(validationResult.ToString()));
            }

            if (leaveRequest.IsCancelled)
            {
                return new FailureResult<LeaveRequest>(DomainErrors.LeaveRequest.InvalidApprovalStateIsCanceled);
            }

            if (command.Approved)
            {
                leaveRequest.Approve();
            }
            else
            {
                leaveRequest.Reject();
            }

            // if request is approved, get and update the employee's allocation
            if (leaveRequest.IsApproved is true)
            {
                LeaveAllocation allocation = await _leaveAllocationRepository.GetEmployeeAllocation(
                    leaveRequest.RequestingEmployeeId,
                    leaveRequest.LeaveTypeId);

                Result updateNumberOfDaysResult = allocation.ChangeNumberOfDays(allocation.NumberOfDays - leaveRequest.DaysRequested());

                if (updateNumberOfDaysResult.IsFailure)
                {
                    return new FailureResult<LeaveRequest>(updateNumberOfDaysResult.Error);
                }

                await _leaveAllocationRepository.UpdateAsync(allocation);
            }

            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            return new SuccessResult<LeaveRequest>(leaveRequest);
        }
    }
}
