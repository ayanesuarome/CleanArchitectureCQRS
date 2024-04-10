namespace CleanArch.Api.Features.LeaveAllocations;

public abstract record BaseCommnad
{
    public int LeaveTypeId { get; set; }
    public int NumberOfDays { get; set; }
    public int Period { get; set; }
}
