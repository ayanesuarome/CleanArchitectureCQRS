namespace CleanArch.Contracts.LeaveTypes;

public sealed record CreateLeaveTypeRequest(string Name, int DefaultDays)
{
}
