namespace CleanArch.Application.Contracts;

public record EmailMessageTemplate
{
    public string To { get; set; } = null!;
    public string TemplateId { get; set; } = null!;
    public virtual dynamic TemplateData { get; set; } = null!;
}
