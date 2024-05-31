using CleanArch.Infrastructure.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CleanArch.Infrastructure.BackgroundJobs;

/// <summary>
/// Represents the <see cref="ProcessOutboxMessagesOptions"/> setup.
/// </summary>
internal sealed class IntegrationEventProcessorJobOptionsSetup(IConfiguration configuration)
    : IConfigureOptions<IntegrationEventProcessorJobOptions>
{
    private const string SectionName = "BackgroundServiceBus:IntegrationEventProcessor";

    /// <inheritdoc />
    public void Configure(IntegrationEventProcessorJobOptions options) => configuration.GetSection(SectionName).Bind(options);
}
