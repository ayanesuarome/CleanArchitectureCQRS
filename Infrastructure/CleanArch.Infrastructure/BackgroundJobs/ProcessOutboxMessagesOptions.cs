namespace CleanArch.Infrastructure.BackgroundJobs;

/// <summary>
/// Represents the <see cref="ProcessOutboxMessagesJob"/> options.
/// </summary>
/// <param name="RetryCount">Retry count.</param>
/// <param name="BatchSize">Amount of messages to bring from data source per read.</param>
internal sealed record ProcessOutboxMessagesOptions
{
    /// <summary>
    /// Gets interval in seconds.
    /// </summary>
    public int IntervalInSeconds { get; init; }

    public int RetryCount { get; init; }

    public int BatchSize { get; init; }
}
