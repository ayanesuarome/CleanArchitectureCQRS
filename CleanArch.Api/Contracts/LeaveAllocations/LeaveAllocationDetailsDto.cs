using CleanArch.Api.Contracts.LeaveTypes;

namespace CleanArch.Api.Contracts.LeaveAllocations;

public sealed record LeaveAllocationDetailsDto : BaseDto
{
    public int NumberOfDays { get; set; }
    public int Period { get; set; }
    public int LeaveTypeId { get; set; }
    public LeaveTypeDetailDto? LeaveType { get; set; }
}
