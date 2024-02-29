using CleanArch.Application.Features.Shared;

namespace CleanArch.Application.Features.LeaveTypeDetails.Queries.GetLeaveTypesDetails;

public record LeaveTypeDetailsDto : BaseDto
{
    public string Name { get; set; } = null!;
    public int DefaultDays { get; set; }
}