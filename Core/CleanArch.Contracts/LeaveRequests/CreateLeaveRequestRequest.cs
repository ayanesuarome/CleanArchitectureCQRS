namespace CleanArch.Contracts.LeaveRequests;

public sealed record CreateLeaveRequestRequest(
    Guid LeaveTypeId,
    DateOnly StartDate,
    DateOnly EndDate,
    string? Comments)
{
}
