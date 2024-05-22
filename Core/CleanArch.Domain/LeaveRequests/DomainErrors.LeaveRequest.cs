using CleanArch.Domain.Core.Primitives.Result;

namespace CleanArch.Domain.Errors;

public static partial class DomainErrors
{
    public static class LeaveRequest
    {
        public static Error NotFound(Guid id) => new("LeaveRequest.NotFound", $"The leave request with Id '{id}' was not found.");
        public static Error LeaveTypeMustExist => new("LeaveRequest.LeaveTypeMustExist", "There must be an associated Leave type.");
        
        public static Error ApprovalStateIsAlreadyCanceled => new(
            "LeaveRequest.ApprovalStateIsAlreadyCanceled",
            "This leave request has been cancelled and its approval state cannot be updated.");

        public static Error NoAllocationsForLeaveType(Guid leaveTypeId) => new(
            "LeaveRequest.NoAllocationsForLeaveType",
            $"You do not have any allocation for this leave type ID {leaveTypeId}.");

        public static Error NotEnoughDays => new("LeaveRequest.NotEnoughDays", "You do not have enough available days for this request.");

        public static Error AlreadyRejected => new("LeaveRequest.AlreadyRejected", "The leave request has already been rejected.");
        public static Error AlreadyApproved => new("LeaveRequest.AlreadyApproved", "The leave request has already been approved.");
        public static Error AlreadyCanceled => new("LeaveRequest.AlreadyCanceled", "The leave request has already been canceled.");
    }
}
