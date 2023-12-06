namespace CleanArch.Application.Features.LeaveTypes.Queries.GetAllLeaveTypes;

public class LeaveTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int DefaultDays { get; set; }
}