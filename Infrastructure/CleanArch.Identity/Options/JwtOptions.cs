﻿namespace CleanArch.Identity.Options;

/// <summary>
/// Represents the JWT configuration settings.
/// </summary>
public record JwtOptions
{
    /// <summary>
    /// Gets or sets the security key.
    /// </summary>
    public string Key { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the issuer.
    /// </summary>
    public string Issuer { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the audience.
    /// </summary>
    public string Audience { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the token expiration time in minutes.
    /// </summary>
    public int DurationInMinutes { get; init; }
}
