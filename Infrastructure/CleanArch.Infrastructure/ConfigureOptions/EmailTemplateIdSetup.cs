using CleanArch.Infrastructure.Services.Emails.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CleanArch.Infrastructure.ConfigureOptions;

internal sealed class EmailTemplateIdSetup(IConfiguration configuration) : IConfigureOptions<EmailTemplateIds>
{
    private const string SectionName = nameof(EmailTemplateIds);
    private readonly IConfiguration _configuration = configuration;

    public void Configure(EmailTemplateIds options)
    {
        _configuration
            .GetRequiredSection(SectionName)
            .Bind(options);
    }
}
