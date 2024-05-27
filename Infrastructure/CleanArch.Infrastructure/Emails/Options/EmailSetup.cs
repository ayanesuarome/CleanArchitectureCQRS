using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CleanArch.Infrastructure.Emails.Options;

internal sealed class EmailSetup(IConfiguration configuration) : IConfigureOptions<EmailOptions>
{
    private const string SectionName = nameof(EmailOptions);
    private readonly IConfiguration _configuration = configuration;

    public void Configure(EmailOptions options)
    {
        _configuration
            .GetRequiredSection(SectionName)
            .Bind(options);
    }
}
