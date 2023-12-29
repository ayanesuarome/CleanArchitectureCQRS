namespace CleanArch.Application.Features.LeaveRequests.Shared;

public abstract class BaseLeaveRequestCommand
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string RequestingEmployeeId { get; set; } = null!;
}
