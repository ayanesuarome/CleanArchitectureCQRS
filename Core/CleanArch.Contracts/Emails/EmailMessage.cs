﻿namespace CleanArch.Api.Contracts.Emails;

public record EmailMessage
{
    public string To { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
}
