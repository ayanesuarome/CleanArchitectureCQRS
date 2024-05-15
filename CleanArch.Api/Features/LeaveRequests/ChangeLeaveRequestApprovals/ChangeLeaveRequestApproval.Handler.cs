using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;

namespace CleanArch.Api.Features.LeaveRequests.ChangeLeaveRequestApprovals;

public static partial class ChangeLeaveRequestApproval
{
    internal sealed class Handler : ICommandHandler<Command, LeaveRequest>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public Handler(
            ILeaveRequestRepository leaveRequestRepository,
            ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task<Result<LeaveRequest>> Handle(Command command, CancellationToken cancellationToken)
        {
            LeaveRequest leaveRequest = await _leaveRequestRepository.GetByIdAsync(command.Id);

            if (leaveRequest is null)
            {
                return new NotFoundResult<LeaveRequest>(DomainErrors.LeaveRequest.NotFound(command.Id));
            }

            if (leaveRequest.IsCancelled)
            {
                return new FailureResult<LeaveRequest>(DomainErrors.LeaveRequest.ApprovalStateIsAlreadyCanceled);
            }

            Result approvalResult = command.Approved
                ? leaveRequest.Approve()
                : leaveRequest.Reject();

            if(approvalResult.IsFailure)
            {
                return new FailureResult<LeaveRequest>(approvalResult.Error);
            }

            // if request is approved, get and update the employee's allocation
            if (leaveRequest.IsApproved is true)
            {
                LeaveAllocation allocation = await _leaveAllocationRepository.GetEmployeeAllocation(
                    leaveRequest.RequestingEmployeeId,
                    leaveRequest.LeaveTypeId);

                Result updateNumberOfDaysResult = allocation.ChangeNumberOfDays(allocation.NumberOfDays - leaveRequest.DaysRequested);

                if (updateNumberOfDaysResult.IsFailure)
                {
                    return new FailureResult<LeaveRequest>(updateNumberOfDaysResult.Error);
                }

                _leaveAllocationRepository.Update(allocation);
            }

            _leaveRequestRepository.Update(leaveRequest);

            return new SuccessResult<LeaveRequest>(leaveRequest);
        }
    }
}
