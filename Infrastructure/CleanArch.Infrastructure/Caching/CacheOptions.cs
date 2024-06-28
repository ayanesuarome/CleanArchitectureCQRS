namespace CleanArch.Infrastructure.Caching;

/// <summary>
/// Represents the caching options.
/// </summary>
internal sealed record CacheOptions
{
    /// <summary>
    /// Gets default expiration in minutes.
    /// </summary>
    public int DefaultExpiration { get; init; }
}
