using CleanArch.Application.Features.LeaveTypes.Queries.GetLeaveTypeList;

namespace CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestList;

public class LeaveRequestDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool? IsApproved { get; set; }
    public DateTime DateRequested { get; set; }
    public LeaveTypeDto? LeaveType { get; set; }
    public string RequestingEmployeeId { get; set; } = null!;
}
