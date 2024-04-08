using CleanArch.Application.Features.Shared;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;

public record LeaveTypeDetailsDto : BaseDto
{
    public string Name { get; set; } = null!;
    public int DefaultDays { get; set; }
}