using CleanArch.Application.Features.LeaveTypes.Queries.GetLeaveTypeList;

namespace CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationList;

public class LeaveAllocationDto
{
    public int Id { get; set; }
    public int NumberOfDays { get; set; }
    public int Period { get; set; }
    public int LeaveTypeId { get; set; }
    public LeaveTypeDto? LeaveType { get; set; }
}
