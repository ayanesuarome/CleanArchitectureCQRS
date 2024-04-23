namespace CleanArch.Contracts.LeaveRequests;

public sealed record UpdateLeaveRequestRequest(
    string? Comments,
    DateOnly StartDate,
    DateOnly EndDate)
{
}
