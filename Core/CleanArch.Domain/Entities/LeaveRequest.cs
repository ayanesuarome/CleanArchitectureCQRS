namespace CleanArch.Domain.Entities;

public class LeaveRequest : BaseEntity<int>
{
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public int LeaveTypeId { get; set; }
    public LeaveType? LeaveType { get; set; }
    public DateTimeOffset DateRequested { get; set; }
    public string? RequestComments { get; set; }
    public bool? IsApproved { get; set; }
    public bool IsCancelled { get; set; }
    public string RequestingEmployeeId { get; set; } = null!;

    public int GetDaysRequested() => (int)(EndDate - StartDate).TotalDays;
}
