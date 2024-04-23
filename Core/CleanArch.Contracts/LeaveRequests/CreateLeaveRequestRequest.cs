namespace CleanArch.Contracts.LeaveRequests;

public sealed record CreateLeaveRequestRequest(
    int LeaveTypeId,
    string? Comments,
    DateOnly StartDate,
    DateOnly EndDate)
{
}
