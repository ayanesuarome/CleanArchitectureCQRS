namespace CleanArch.Contracts.LeaveTypes;

public sealed record LeaveTypeDetailDto(
    Guid Id,
    string Name,
    int DefaultDays,
    DateTimeOffset? DateCreated)
{
}