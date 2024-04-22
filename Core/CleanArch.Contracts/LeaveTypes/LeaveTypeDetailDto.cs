namespace CleanArch.Contracts.LeaveTypes;

public sealed record LeaveTypeDetailDto(
    int Id,
    string Name,
    int DefaultDays,
    DateTimeOffset? DateCreated)
{
}