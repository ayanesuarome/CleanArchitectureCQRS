using CleanArch.Api.Contracts.LeaveTypes;
using CleanArch.Application.Models.Identity;

namespace CleanArch.Api.Contracts.LeaveRequests;

public sealed record LeaveRequestDetailsDto : BaseDto
{
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public string? RequestComments { get; set; }
    public bool? IsApproved { get; set; }
    public bool IsCancelled { get; set; }
    public int LeaveTypeId { get; set; }
    public LeaveTypeDetailDto? LeaveType { get; set; }
    public string RequestingEmployeeId { get; set; }
    public Employee Employee { get; set; }
    public DateTimeOffset DateRequested { get; set; }
    public DateTimeOffset DateActioned { get; set; }
}
