using CleanArch.Domain.Primitives;

namespace CleanArch.Domain.Entities;

public class LeaveType : BaseEntity<int>
{
    public string Name { get; set; }
    public int DefaultDays { get; set; }
}
