using CleanArch.Domain.Core.Primitives;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestList;

public static partial class GetLeaveRequestList
{
    internal sealed record Response
    {
        public Response(PagedList<Model> leaveRequests) => LeaveRequests = leaveRequests;

        public PagedList<Model> LeaveRequests { get; }

        public sealed record Model(
            Guid Id,
            string StartDate,
            string EndDate,
            string LeaveTypeName,
            bool? IsApproved,
            bool IsCancelled,
            DateTimeOffset DateCreated)
        {
        }
    }
}
