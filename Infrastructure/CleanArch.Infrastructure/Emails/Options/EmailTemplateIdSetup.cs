using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CleanArch.Infrastructure.Emails.Options;

internal sealed class EmailTemplateIdSetup(IConfiguration configuration) : IConfigureOptions<EmailTemplateIdOptions>
{
    private const string SectionName = nameof(EmailTemplateIdOptions);
    private readonly IConfiguration _configuration = configuration;

    public void Configure(EmailTemplateIdOptions options)
    {
        _configuration
            .GetRequiredSection(SectionName)
            .Bind(options);
    }
}
