namespace CleanArch.Infrastructure.Emails;

public record EmailMessageResponse(string RecipientName, DateOnly Start, DateOnly End, DateTimeOffset Now)
{
    public bool? IsApproved { get; set; }
}
