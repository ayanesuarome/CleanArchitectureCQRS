namespace CleanArch.Api.Contracts.Emails;

public record EmailMessageDto(string RecipientName, DateOnly Start, DateOnly End, DateTimeOffset Now)
{
    public bool? IsApproved { get; set; }
}
