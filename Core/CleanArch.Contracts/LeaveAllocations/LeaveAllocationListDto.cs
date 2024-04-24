namespace CleanArch.Contracts.LeaveAllocations;

public sealed record LeaveAllocationListDto
{
    public LeaveAllocationListDto(IReadOnlyCollection<LeaveAllocationModel> leaveAllocationList) => LeaveAllocationList = leaveAllocationList;
    public IReadOnlyCollection<LeaveAllocationModel> LeaveAllocationList { get; }

    public sealed record LeaveAllocationModel(Guid Id, int NumberOfDays, int Period, Guid LeaveTypeId)
    {
    }
}
