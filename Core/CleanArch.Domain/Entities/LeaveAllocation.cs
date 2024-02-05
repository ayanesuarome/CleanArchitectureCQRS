namespace CleanArch.Domain.Entities;

public class LeaveAllocation : BaseEntity
{
    public int NumberOfDays { get; set; }
    public int Period { get; set; }
    public int LeaveTypeId { get; set; }
    public LeaveType? LeaveType { get; set; }
    public string EmployeeId { get; set; } = null!;

    public bool HasEnoughDays(DateTimeOffset start, DateTimeOffset end)
    {
        return (int)(end - start).TotalDays < NumberOfDays;
    }
}
