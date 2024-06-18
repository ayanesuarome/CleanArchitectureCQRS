namespace CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;

public static partial class CreateLeaveRequest
{
    public sealed record CreateRequest(Guid LeaveTypeId, string StartDate, string EndDate, string? Comments);
}
