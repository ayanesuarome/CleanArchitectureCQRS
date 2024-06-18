using CleanArch.Domain.Core.Primitives;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestList;

public static partial class GetLeaveRequestList
{
    public sealed record Response
    {
        public Response(PagedList<Model> leaveRequests) => LeaveRequests = leaveRequests;

        public PagedList<Model> LeaveRequests { get; }

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
