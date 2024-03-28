using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.Features.LeaveRequests.Shared;

public static class LeaveRequestErrors
{
    public static Error NotFound(int id) => new Error("LeaveRequests.NotFound", $"{nameof(LeaveRequest)} with Id '{id}' not found");

    public static Error InvalidLeaveRequest(IDictionary<string, string[]> errors)
    {
        return new Error($"{nameof(LeaveRequest)}.InvalidLeaveRequest", $"Invalid {nameof(LeaveRequest)}", errors);
    }

    public static Error InvalidApprovalRequest(IDictionary<string, string[]> errors)
    {
        return new Error("LeaveRequest.InvalidApprovalRequest", $"Invalid approval request", errors);
    }

    public static Error InvalidApprovalStateIsCanceled() => new Error("LeaveRequest.InvalidApprovalStateIsCanceled", "This leave request has been cancelled and its approval state cannot be updated");
    
    public static Error InvalidNumberOfDays(int daysRequested) => new Error("LeaveRequest.InvalidNumberOfDays", $"The employee's allocation does not have {daysRequested} available number of days");

    private static string GetErrorType(string type)
    {
        return $"{nameof(LeaveRequest)}.{type}";
    }
}
