using CleanArch.Infrastructure.BackgroundJobs;

namespace CleanArch.Infrastructure.Events;

/// <summary>
/// Represents the <see cref="IntegrationEventProcessorJobOptions"/> options.
/// </summary>
internal sealed record IntegrationEventProcessorJobOptions
{
    /// <summary>
    /// Gets interval in seconds.
    /// </summary>
    public int IntervalInSeconds { get; init; }

    /// <summary>
    /// Gets retry count.
    /// </summary>
    public int RetryCount { get; init; }
}
