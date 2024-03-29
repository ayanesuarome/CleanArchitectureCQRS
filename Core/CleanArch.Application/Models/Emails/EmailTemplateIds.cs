﻿namespace CleanArch.Application.Models.Emails;

public record EmailTemplateIds
{
    public string LeaveRequestApproval { get; set; } = null!;
    public string LeaveRequestCancelation { get; set; } = null!;
    public string LeaveRequestCreate { get; set; } = null!;
    public string LeaveRequestUpdate { get; set; } = null!;
}
