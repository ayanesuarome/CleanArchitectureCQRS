using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using CleanArch.Domain.Errors;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.ValueObjects;

namespace CleanArch.Api.Features.LeaveRequests.CancelLeaveRequests;

public static partial class CancelLeaveRequest
{
    internal sealed class Handler : ICommandHandler<Command, Result<LeaveRequest>>
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
            LeaveRequest leaveRequest = await _leaveRequestRepository.GetByIdAsync(new LeaveRequestId(command.Id));

            if (leaveRequest is null)
            {
                return new NotFoundResult<LeaveRequest>(DomainErrors.LeaveRequest.NotFound(command.Id));
            }

            Result cancelResult = leaveRequest.Cancel();

            if(cancelResult.IsFailure)
            {
                return Result.Failure<LeaveRequest>(cancelResult.Error);
            }

            _leaveRequestRepository.Update(leaveRequest);

            // if already approved, re-evaluate the employee's allocations for the leave type
            if (leaveRequest.IsApproved is true)
            {
                LeaveAllocation allocation = await _leaveAllocationRepository.GetEmployeeAllocation(
                    leaveRequest.RequestingEmployeeId,
                    leaveRequest.LeaveTypeId);

                Result updateNumberOfDaysResult = allocation.ChangeNumberOfDays(allocation.NumberOfDays + leaveRequest.DaysRequested);

                if(updateNumberOfDaysResult.IsFailure)
                {
                    return Result.Failure<LeaveRequest>(updateNumberOfDaysResult.Error);
                }

                _leaveAllocationRepository.Update(allocation);
            }

            return Result.Success<LeaveRequest>(leaveRequest);
        }
    }
}
