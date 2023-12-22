namespace CleanArch.Application.Features.LeaveAllocations.Shared;

public abstract class BaseLeaveAllocationCommnad
{
    public int NumberOfDays { get; set; }
    public int Period { get; set; }
    public int LeaveTypeId { get; set; }
}
