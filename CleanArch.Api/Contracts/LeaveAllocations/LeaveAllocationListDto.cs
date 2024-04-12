namespace CleanArch.Api.Contracts.LeaveAllocations;

public sealed record LeaveAllocationListDto
{
    public LeaveAllocationListDto(IReadOnlyCollection<LeaveAllocationModel> leaveAllocationList) => LeaveAllocationList = leaveAllocationList;
    public IReadOnlyCollection<LeaveAllocationModel> LeaveAllocationList { get; }

    public sealed record LeaveAllocationModel(int Id, int NumberOfDays, int Period, int LeaveTypeId, LeaveAllocationDetailsDto? LeaveType)
    {
    }
}
