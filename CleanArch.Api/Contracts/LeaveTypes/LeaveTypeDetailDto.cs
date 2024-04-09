namespace CleanArch.Api.Contracts.LeaveTypes;

public sealed record LeaveTypeDetailDto(string Name, int DefaultDays) : BaseDto
{
}