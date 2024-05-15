namespace CleanArch.Contracts.LeaveRequests;

public sealed record UpdateLeaveRequestRequest(
    string? Comments,
    string StartDate,
    string EndDate)
{
}
