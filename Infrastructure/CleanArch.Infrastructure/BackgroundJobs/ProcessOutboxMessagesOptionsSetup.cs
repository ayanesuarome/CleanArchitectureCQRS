using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CleanArch.Infrastructure.BackgroundJobs;

/// <summary>
/// Represents the <see cref="ProcessOutboxMessagesOptions"/> setup.
/// </summary>
internal sealed class ProcessOutboxMessagesOptionsSetup(IConfiguration configuration)
    : IConfigureOptions<ProcessOutboxMessagesOptions>
{
    private const string SectionName = "BackgroundJobs:ProcessOutboxMessages";

    /// <inheritdoc />
    public void Configure(ProcessOutboxMessagesOptions options) => configuration.GetSection(SectionName).Bind(options);
}
