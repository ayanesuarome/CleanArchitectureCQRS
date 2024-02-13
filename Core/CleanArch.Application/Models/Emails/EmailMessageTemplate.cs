namespace CleanArch.Application.Models.Emails;

public class EmailMessageTemplate
{
    public string To { get; set; } = null!;
    public string TemplateId { get; set; } = null!;
    public virtual dynamic TemplateData { get; set; } = null!;
}
