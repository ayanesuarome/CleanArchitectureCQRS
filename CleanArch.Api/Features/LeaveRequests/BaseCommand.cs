namespace CleanArch.Api.Features.LeaveRequests;

public abstract record BaseCommand
{
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
}
