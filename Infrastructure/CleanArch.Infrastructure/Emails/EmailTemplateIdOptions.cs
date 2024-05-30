namespace CleanArch.Infrastructure.Emails;

public sealed record EmailTemplateIdOptions
{
    public string LeaveRequestApproval { get; init; } = string.Empty;
    public string LeaveRequestCancelation { get; init; } = string.Empty;
    public string LeaveRequestCreate { get; init; } = string.Empty;
    public string LeaveRequestUpdate { get; init; } = string.Empty;
}
