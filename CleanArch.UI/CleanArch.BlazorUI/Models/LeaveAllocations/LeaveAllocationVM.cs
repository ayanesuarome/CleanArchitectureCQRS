using CleanArch.BlazorUI.Models.LeaveTypes;

namespace CleanArch.BlazorUI.Models.LeaveAllocations;

public class LeaveAllocationVM
{
    public int NumberOfDays { get; set; }
    public int Period { get; set; }
    public LeaveTypeVM? LeaveType { get; set; }
}
