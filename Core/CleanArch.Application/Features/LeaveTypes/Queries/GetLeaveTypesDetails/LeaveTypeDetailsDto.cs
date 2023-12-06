namespace CleanArch.Application.Features.LeaveTypesDetails.Queries.GetLeaveTypesDetails;

public class LeaveTypeDetailsDto : BaseEntityDto
{
    public string Name { get; set; } = null!;
    public int DefaultDays { get; set; }
}