using CleanArch.Application.Features.LeaveTypes.Queries.GetLeaveTypeList;
using CleanArch.Application.Features.Shared;

namespace CleanArch.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationDetails;

public record LeaveAllocationDetailsDto : BaseDto
{
    public int NumberOfDays { get; set; }
    public int Period { get; set; }
    public int LeaveTypeId { get; set; }
    public LeaveTypeDto? LeaveType { get; set; }
}
