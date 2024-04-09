using CleanArch.Application.Features.LeaveTypes.Queries.GetLeaveTypeList;
using CleanArch.Application.Features.Shared;
using CleanArch.Application.Models.Identity;

namespace CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestDetails;

public record LeaveRequestDetailsDto : BaseDto
{
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public int LeaveTypeId { get; set; }
    public LeaveTypeDto? LeaveType { get; set; }
    public DateTimeOffset DateRequested { get; set; }
    public string? RequestComments { get; set; }
    public bool? IsApproved { get; set; }
    public bool IsCancelled { get; set; }
    public string RequestingEmployeeId { get; set; } = null!;
    public Employee Employee { get; set; }
    public DateTimeOffset DateActioned { get; set; }
}
