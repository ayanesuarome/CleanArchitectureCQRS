namespace CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;

public static partial class CreateLeaveRequest
{
    public sealed record Request(Guid LeaveTypeId, string StartDate, string EndDate, string? Comments);
}
