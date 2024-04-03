using CleanArch.Application.Exceptions;
using CleanArch.Application.Extensions;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, LeaveRequest>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IValidator<ChangeLeaveRequestApprovalCommand> _validator;

    public ChangeLeaveRequestApprovalCommandHandler(
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveAllocationRepository leaveAllocationRepository,
        IValidator<ChangeLeaveRequestApprovalCommand> validator)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveAllocationRepository = leaveAllocationRepository;
        _validator = validator;
    }

    public async Task<LeaveRequest> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        LeaveRequest leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            throw new BadRequestException("Invalid approval request", validationResult);
        }

        if(leaveRequest.IsCancelled)
        {
            validationResult.AddError(
                nameof(leaveRequest.IsCancelled),
                "This leave request has been cancelled and its approval state cannot be updated");

            throw new BadRequestException($"Invalid {nameof(LeaveRequest)}", validationResult);
        }
        
        leaveRequest.IsApproved = request.Approved;
        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        // if request is approved, get and update the employee's allocation
        if(leaveRequest.IsApproved is true)
        {
            LeaveAllocation allocation = await _leaveAllocationRepository.GetEmployeeAllocation(
                leaveRequest.RequestingEmployeeId,
                leaveRequest.LeaveTypeId);
            allocation.UpdateNumberOfDays(allocation.NumberOfDays - leaveRequest.GetDaysRequested());

            await _leaveAllocationRepository.UpdateAsync(allocation);
        }

        return leaveRequest;
    }
}
