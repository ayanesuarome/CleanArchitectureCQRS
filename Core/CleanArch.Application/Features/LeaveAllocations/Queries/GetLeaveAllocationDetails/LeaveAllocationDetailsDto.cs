using CleanArch.Application.Features.LeaveTypes.Queries.GetAllLeaveTypes;

namespace CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationDetails;

public class LeaveAllocationDetailsDto : BaseDto
{
    public int NumberOfDays { get; set; }
    public int Period { get; set; }
    public int LeaveTypeId { get; set; }
    public LeaveTypeDto? LeaveType { get; set; }
}
