namespace CleanArch.Application.Models.Emails;

public record EmailSettings
{
    public string ApiKey { get; set; } = null!;
    public string FromAddress { get; set; } = null!;
    public string FromName { get; set; } = null!;
    public string ReplyTo { get; set; } = null!;
}
