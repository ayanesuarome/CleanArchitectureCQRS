using CleanArch.Application.Models.Emails;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CleanArch.Infrastructure.ConfigureOptions;

internal sealed class EmailSettingSetup(IConfiguration configuration) : IConfigureOptions<EmailSettings>
{
    private const string SectionName = nameof(EmailSettings);
    private readonly IConfiguration _configuration = configuration;

    public void Configure(EmailSettings options)
    {
        _configuration
            .GetRequiredSection(SectionName)
            .Bind(options);
    }
}
