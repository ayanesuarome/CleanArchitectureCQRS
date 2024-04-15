using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Domain.Errors;

public static partial class DomainErrors
{
    public static class LeaveRequest
    {
        public static Error NotFound(int id) => new Error("LeaveRequest.NotFound", $"The leave request with Id '{id}' was not found.");
        public static Error LeaveTypeMustExist => new Error("LeaveRequest.LeaveTypeMustExist", "There must be an associated Leave type.");
        
        public static Error InvalidApprovalStateIsCanceled => new Error(
            "LeaveRequest.InvalidApprovalStateIsCanceled",
            "This leave request has been cancelled and its approval state cannot be updated.");

        public static Error InvalidNumberOfDays(int daysRequested) => new Error(
            "LeaveRequest.InvalidNumberOfDays",
            $"The employee's allocation does not have {daysRequested} available number of days.");

        public static Error NoAllocationsForLeaveType(int leaveTypeId) => new Error(
            "LeaveRequest.NoAllocationsForLeaveType",
            $"You do not have any allocation for this leave type ID {leaveTypeId}.");

        public static Error NotEnoughDays => new Error(
            "LeaveRequest.NotEnoughDays",
            "You do not have enough available days for this request.");
    }
}
