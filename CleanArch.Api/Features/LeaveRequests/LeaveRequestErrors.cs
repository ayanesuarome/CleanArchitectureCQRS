using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CleanArch.Api.Features.LeaveRequests;

public static class LeaveRequestErrors
{
    public static Error LeaveTypeMustExist() => new Error("LeaveRequest.LeaveTypeMustExist", "There must be an associated Leave type.");
    public static Error RequestCommentsMaximumLength(string length) => new Error("LeaveRequest.RequestCommentsMaximumLength", $"The {nameof(LeaveRequest.RequestComments)} must be up to {length}.");
    public static Error StartDateLowerThanEndDate() => new Error("LeaveRequest.StartDateMustBeLowerThanEndDate", $"The {nameof(LeaveRequest.StartDate)} must be before {nameof(LeaveRequest.EndDate)}.");
    public static Error EndDateGeatherThanStartDate() => new Error("LeaveRequest.EndDateGeatherThanStartDate", $"The {nameof(LeaveRequest.EndDate)} must be after {nameof(LeaveRequest.StartDate)}.");
    public static Error IdRequired() => new Error("LeaveRequest.IdRequired", $"The {nameof(LeaveRequest.Id)} is required.");

    public static Error NotFound(int id) => new Error("LeaveRequest.NotFound", $"Leave request with Id '{id}' not found.");

    public static Error InvalidLeaveRequest(IDictionary<string, string[]> errors)
    {
        return new Error("LeaveRequest.InvalidLeaveRequest", "Invalid Leave request", errors);
    }

    public static Error InvalidApprovalRequest(IDictionary<string, string[]> errors)
    {
        return new Error("LeaveRequest.InvalidApprovalRequest", "Invalid approval request", errors);
    }

    public static Error CreateLeaveRequestValidation(string message)
    {
        return new Error($"CreateLeaveRequest.Validation", message);
    }
    
    public static Error UpdateLeaveRequestValidation(string message)
    {
        return new Error($"UpdateLeaveRequest.Validation", message);
    }

    public static Error InvalidApprovalStateIsCanceled() => new Error("LeaveRequest.InvalidApprovalStateIsCanceled", "This leave request has been cancelled and its approval state cannot be updated.");

    public static Error InvalidNumberOfDays(int daysRequested) => new Error("LeaveRequest.InvalidNumberOfDays", $"The employee's allocation does not have {daysRequested} available number of days.");

    public static Error NoAllocationsForLeaveType(int leaveTypeId) => new Error("LeaveRequest.NoAllocationsForLeaveType", $"You do not have any allocation for this leave type ID {leaveTypeId}.");

    public static Error NotEnoughDays() => new Error("LeaveRequest.NotEnoughDays", "You do not have enough available days for this request.");
}
