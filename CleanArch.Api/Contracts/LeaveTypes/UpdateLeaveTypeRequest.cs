namespace CleanArch.Api.Contracts.LeaveTypes;

public sealed record UpdateLeaveTypeRequest(string? Name, int DefaultDays);
