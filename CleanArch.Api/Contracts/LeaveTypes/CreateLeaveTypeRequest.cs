namespace CleanArch.Api.Contracts.LeaveTypes;

public sealed record CreateLeaveTypeRequest(string Name, int DefaultDays)
{
}
