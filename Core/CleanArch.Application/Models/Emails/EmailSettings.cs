namespace CleanArch.Application.Models.Emails;

public record EmailSettings
{
    public string ApiKey { get; init; } = null!;
    public string FromAddress { get; init; } = null!;
    public string FromName { get; init; } = null!;
    public string ReplyTo { get; init; } = null!;
}
