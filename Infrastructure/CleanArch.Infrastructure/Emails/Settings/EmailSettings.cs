namespace CleanArch.Infrastructure.Emails.Settings;

internal sealed record EmailSettings
{
    public string ApiKey { get; init; } = string.Empty;
    public string FromAddress { get; init; } = string.Empty;
    public string FromName { get; init; } = string.Empty;
    public string ReplyTo { get; init; } = string.Empty;
}
