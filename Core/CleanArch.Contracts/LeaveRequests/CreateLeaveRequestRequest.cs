namespace CleanArch.Contracts.LeaveRequests;

public sealed record CreateLeaveRequestRequest(
    Guid LeaveTypeId,
    string StartDate,
    string EndDate,
    string? Comments)
{
}
