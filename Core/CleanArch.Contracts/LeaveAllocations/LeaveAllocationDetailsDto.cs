namespace CleanArch.Contracts.LeaveAllocations;

public sealed record LeaveAllocationDetailsDto(
    int NumberOfDays,
    int Period,
    Guid LeaveTypeId)
{
}
