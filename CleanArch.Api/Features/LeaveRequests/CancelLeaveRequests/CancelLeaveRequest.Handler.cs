using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.CancelLeaveRequests;

public static partial class CancelLeaveRequest
{
    internal sealed class Handler : IRequestHandler<Command, Result<LeaveRequest>>
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
                return new NotFoundResult<LeaveRequest>(LeaveRequestErrors.NotFound(command.Id));
            }

            leaveRequest.IsCancelled = true;
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            // if already approved, re-evaluate the employee's allocations for the leave type
            if (leaveRequest.IsApproved is true)
            {
                LeaveAllocation allocation = await _leaveAllocationRepository.GetEmployeeAllocation(
                    leaveRequest.RequestingEmployeeId,
                    leaveRequest.LeaveTypeId);

                bool canUpdate = allocation.UpdateNumberOfDays(allocation.NumberOfDays + leaveRequest.GetDaysRequested());

                await _leaveAllocationRepository.UpdateAsync(allocation);
            }

            return new SuccessResult<LeaveRequest>(leaveRequest);
        }
    }
}
