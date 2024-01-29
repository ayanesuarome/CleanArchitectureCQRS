namespace CleanArch.Application.Features.LeaveAllocations.Shared;

public abstract class BaseLeaveAllocationCommnad
{
    public int LeaveTypeId { get; set; }
    public int NumberOfDays { get; set; }
    public int Period { get; set; }
}
