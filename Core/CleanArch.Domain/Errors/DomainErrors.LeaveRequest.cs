using CleanArch.Domain.Primitives.Result;

namespace CleanArch.Domain.Errors;

public static partial class DomainErrors
{
    public static class LeaveRequest
    {
        public static Error NotFound(Guid id) => new Error("LeaveRequest.NotFound", $"The leave request with Id '{id}' was not found.");
        public static Error LeaveTypeMustExist => new Error("LeaveRequest.LeaveTypeMustExist", "There must be an associated Leave type.");
        
        public static Error ApprovalStateIsAlreadyCanceled => new Error(
            "LeaveRequest.ApprovalStateIsAlreadyCanceled",
            "This leave request has been cancelled and its approval state cannot be updated.");

        public static Error NoAllocationsForLeaveType(Guid leaveTypeId) => new Error(
            "LeaveRequest.NoAllocationsForLeaveType",
            $"You do not have any allocation for this leave type ID {leaveTypeId}.");

        public static Error NotEnoughDays => new Error(
            "LeaveRequest.NotEnoughDays",
            "You do not have enough available days for this request.");

        public static Error AlreadyRejected => new Error("LeaveRequest.AlreadyRejected", "The leave request has already been rejected.");
        public static Error AlreadyApproved => new Error("LeaveRequest.AlreadyApproved", "The leave request has already been approved.");
        public static Error AlreadyCanceled => new Error("LeaveRequest.AlreadyCanceled", "The leave request has already been canceled.");
    }
}
