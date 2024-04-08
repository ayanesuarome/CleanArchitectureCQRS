namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeList;

public record LeaveTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int DefaultDays { get; set; }
}