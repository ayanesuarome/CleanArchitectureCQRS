using CleanArch.Domain.Entities;

namespace CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestList;

public class LeaveRequestDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool? IsApproved { get; set; }
    public DateTime DateRequested { get; set; }
    public LeaveType? LeaveType { get; set; }
    public string RequestingEmployeeId { get; set; } = null!;
}
