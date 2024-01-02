using CleanArch.Application.Features.LeaveTypes.Queries.GetLeaveTypeList;
using CleanArch.Application.Features.Shared;

namespace CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestDetails;

public class LeaveRequestDetailsDto : BaseDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int LeaveTypeId { get; set; }
    public LeaveTypeDto? LeaveType { get; set; }
    public DateTime DateRequested { get; set; }
    public string? RequestComments { get; set; }
    public bool? IsApproved { get; set; }
    public bool IsCancelled { get; set; }
    public string RequestingEmployeeId { get; set; } = null!;
    public DateTime DateActioned { get; set; }
}
