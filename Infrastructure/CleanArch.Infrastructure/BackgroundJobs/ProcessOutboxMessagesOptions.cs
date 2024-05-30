namespace CleanArch.Infrastructure.BackgroundJobs;

/// <summary>
/// Represents the <see cref="ProcessOutboxMessagesJob"/> options.
/// </summary>
internal sealed record ProcessOutboxMessagesOptions
{
    /// <summary>
    /// Gets interval in seconds.
    /// </summary>
    public int IntervalInSeconds { get; init; }

    /// <summary>
    /// Gets retry count.
    /// </summary>
    public int RetryCount { get; init; }

    /// <summary>
    /// Gets size of batches.
    /// </summary>
    public int BatchSize { get; init; }
}
