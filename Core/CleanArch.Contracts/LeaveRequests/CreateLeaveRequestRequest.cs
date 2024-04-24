namespace CleanArch.Contracts.LeaveRequests;

public sealed record CreateLeaveRequestRequest(
    Guid LeaveTypeId,
    string? Comments,
    DateOnly StartDate,
    DateOnly EndDate)
{
}
