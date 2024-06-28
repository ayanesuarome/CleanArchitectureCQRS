using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CleanArch.Infrastructure.Emails;

internal sealed class EmailOptionsSetup(IConfiguration configuration)
    : IConfigureOptions<EmailOptions>
{
    private const string SectionName = "Email";

    /// <inheritdoc />
    public void Configure(EmailOptions options) => configuration.GetRequiredSection(SectionName).Bind(options);
}
