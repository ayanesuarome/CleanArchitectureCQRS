namespace CleanArch.Api.Features.LeaveRequests.AdminGetLeaveRequestList;

public static partial class AdminGetLeaveRequestList
{
    public sealed record Response
    {
        public Response(IReadOnlyCollection<Model> leaveRequests) => LeaveRequests = leaveRequests;

        public IReadOnlyCollection<Model> LeaveRequests { get; }

        public sealed record Model(
            Guid Id,
            string StartDate,
            string EndDate,
            string? RequestComments,
            Guid LeaveTypeId,
            string LeaveTypeName,
            Guid RequestingEmployeeId,
            string EmployeeFullName,
            bool? IsApproved,
            bool IsCancelled,
            DateTimeOffset DateCreated)
        {
        }
    }
}
