namespace CleanArch.Domain.Entities;

public class LeaveType : BaseEntity
{
    public string Name { get; set; } = null!;
    public int DefaultDays { get; set; }
}
