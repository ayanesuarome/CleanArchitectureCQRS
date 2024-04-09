using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;

namespace CleanArch.Api.Features.LeaveRequests;

public static class LeaveRequestErrors
{
    public static Error LeaveTypeMustExist() => new Error("LeaveRequest.LeaveTypeMustExist", "There must be an associated Leave type.");
    public static Error RequestCommentsMaximumLength(int length) => new Error("LeaveRequest.RequestCommentsMaximumLength", $"{nameof(LeaveRequest.RequestComments)} must be up to {length}.");
    public static Error StartDateLowerThanEndDate(string message) => new Error("LeaveRequest.StartDateMustBeLowerThanEndDate", message);
    public static Error EndDateGeatherThanStartDate(string message) => new Error("LeaveRequest.EndDateGeatherThanStartDate", message);

    public static Error NotFound(int id) => new Error("LeaveRequest.NotFound", $"Leave request with Id '{id}' not found");

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

    public static Error InvalidApprovalStateIsCanceled() => new Error($"{nameof(LeaveRequest)}.InvalidApprovalStateIsCanceled", "This leave request has been cancelled and its approval state cannot be updated");

    public static Error InvalidNumberOfDays(int daysRequested) => new Error($"{nameof(LeaveRequest)}.InvalidNumberOfDays", $"The employee's allocation does not have {daysRequested} available number of days");

    public static Error NoAllocationsForLeaveType(int leaveTypeId) => new Error($"{nameof(LeaveRequest)}.NoAllocationsForLeaveType", $"You do not have any allocation for this leave type ID {leaveTypeId}");

    public static Error NotEnoughDays() => new Error("LeaveRequest.NotEnoughDays", "You do not have enough available days for this request.");
}
