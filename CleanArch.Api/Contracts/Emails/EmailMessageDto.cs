namespace CleanArch.Api.Contracts.Emails;

public record EmailMessageDto(string RecipientName, DateTimeOffset Start, DateTimeOffset End, DateTimeOffset Now)
{
    public bool? IsApproved { get; set; }
}
