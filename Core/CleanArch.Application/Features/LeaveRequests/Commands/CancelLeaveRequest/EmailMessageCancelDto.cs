namespace CleanArch.Application.Features.LeaveRequests.Commands.CancelLeaveRequest;

public class EmailMessageCancelDto
{
    public string? RecipientName { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    public DateTimeOffset Now { get; set; }
}
