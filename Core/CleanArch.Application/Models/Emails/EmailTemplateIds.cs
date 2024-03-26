namespace CleanArch.Application.Models.Emails;

public record EmailTemplateIds
{
    public string LeaveRequestApproval { get; init; } = null!;
    public string LeaveRequestCancelation { get; init; } = null!;
    public string LeaveRequestCreate { get; init; } = null!;
    public string LeaveRequestUpdate { get; init; } = null!;
}
