namespace CleanArch.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;

public class EmailMessageCreateDto
{
    public string? RecipientName { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    public DateTimeOffset Now { get; set; }
}
