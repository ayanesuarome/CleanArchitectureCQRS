using CleanArch.Application.Features.LeaveTypes.Queries.GetLeaveTypeList;
using CleanArch.Application.Models.Identity;

namespace CleanArch.Application.Features.LeaveRequests.Queries.Shared;

public record LeaveRequestDto
{
    public int Id { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public bool? IsApproved { get; set; }
    public DateTimeOffset DateRequested { get; set; }
    public LeaveTypeDto? LeaveType { get; set; }
    public Employee Employee { get; set; }
}
