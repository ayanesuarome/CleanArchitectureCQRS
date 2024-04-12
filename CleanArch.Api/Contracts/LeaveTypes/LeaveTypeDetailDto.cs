namespace CleanArch.Api.Contracts.LeaveTypes;

public sealed record LeaveTypeDetailDto : BaseDto
{
    public string Name { get; set; }
    public int DefaultDays { get; set; }
}