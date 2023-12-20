namespace CleanArch.Application.Features.LeaveTypeDetails.Queries.GetLeaveTypesDetails;

public class LeaveTypeDetailsDto : BaseDto
{
    public string Name { get; set; } = null!;
    public int DefaultDays { get; set; }
}