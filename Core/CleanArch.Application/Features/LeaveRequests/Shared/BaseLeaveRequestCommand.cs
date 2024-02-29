namespace CleanArch.Application.Features.LeaveRequests.Shared;

public abstract record BaseLeaveRequestCommand
{
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
}
