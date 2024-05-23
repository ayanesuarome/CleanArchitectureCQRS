namespace CleanArch.Domain.Core.Time;

/// <summary>
/// Represents the static system time provider.
/// </summary>
public static class SystemTimeProvider
{
    /// <summary>
    /// Gets the current date and time.
    /// </summary>
    public static DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
