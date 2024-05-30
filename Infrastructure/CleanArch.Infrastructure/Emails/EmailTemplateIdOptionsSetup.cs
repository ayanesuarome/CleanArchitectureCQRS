using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CleanArch.Infrastructure.Emails;

internal sealed class EmailTemplateIdOptionsSetup(IConfiguration configuration)
    : IConfigureOptions<EmailTemplateIdOptions>
{
    private const string SectionName = "EmailTemplateIds";

    public void Configure(EmailTemplateIdOptions options) => configuration.GetRequiredSection(SectionName).Bind(options);
}
