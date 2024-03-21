using CleanArch.Application.Exceptions;
using CleanArch.Application.Extensions;
using CleanArch.Application.Features.LeaveRequests.Shared;
using CleanArch.Application.Models;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArch.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Result<LeaveRequest>>
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

    public async Task<Result<LeaveRequest>> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        LeaveRequest leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest == null)
        {
            // TODO: remove throw
            return new NotFoundResult<LeaveRequest>(LeaveRequestErrors.NotFound(request.Id));
            //throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            // TODO: to remove throw
            //return Result<LeaveRequest>.Failure(LeaveRequestErrors.InvalidApprovalRequest(validationResult));
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

        // if request is approved, get and update the employee's allocation
        if(leaveRequest.IsApproved == true)
        {
            LeaveAllocation allocation = await _leaveAllocationRepository.GetEmployeeAllocation(
                leaveRequest.RequestingEmployeeId,
                leaveRequest.LeaveTypeId);
            
            bool canUpdate = allocation.UpdateNumberOfDays(allocation.NumberOfDays - leaveRequest.GetDaysRequested());

            if(!canUpdate)
            {
                // TODO:
                //return Result<LeaveRequest>.Failure(new Error("as","as"));
            }

            await _leaveAllocationRepository.UpdateAsync(allocation);
        }

        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        return new SuccessResult<LeaveRequest>(leaveRequest);
    }
}
