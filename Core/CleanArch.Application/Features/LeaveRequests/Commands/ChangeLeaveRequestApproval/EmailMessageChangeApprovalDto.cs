namespace CleanArch.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval;

public record EmailMessageChangeApprovalDto
{
    public string? RecipientName { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    public bool? IsApproved { get; set; }
    public DateTimeOffset Now { get; set; }
}
