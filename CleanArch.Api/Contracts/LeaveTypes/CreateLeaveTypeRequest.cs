namespace CleanArch.Api.Contracts.LeaveTypes;

public record CreateLeaveTypeRequest(string Name, int DefaultDays)
{
}
