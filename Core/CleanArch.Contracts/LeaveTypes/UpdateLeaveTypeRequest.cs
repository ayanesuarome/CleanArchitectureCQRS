namespace CleanArch.Contracts.LeaveTypes;

public sealed record UpdateLeaveTypeRequest(string? Name, int DefaultDays);
