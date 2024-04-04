using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using MediatR;
using CleanArch.Application.Features.LeaveRequests.Shared;
using CleanArch.Domain.Repositories;

namespace CleanArch.Application.Features.LeaveRequests.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Result<LeaveRequest>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public CancelLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveAllocationRepository = leaveAllocationRepository;
    }

    public async Task<Result<LeaveRequest>> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        LeaveRequest leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
        {
            return new NotFoundResult<LeaveRequest>(LeaveRequestErrors.NotFound(request.Id));
        }

        leaveRequest.IsCancelled = true;
        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        // if already approved, re-evaluate the employee's allocations for the leave type
        if(leaveRequest.IsApproved is true)
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
