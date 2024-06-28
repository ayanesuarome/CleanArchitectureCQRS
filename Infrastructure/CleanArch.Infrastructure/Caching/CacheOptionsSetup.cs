using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CleanArch.Infrastructure.Caching;

/// <summary>
/// Represents the <see cref="CacheOptions"/> setup.
/// </summary>
internal sealed record CacheOptionsSetup(IConfiguration configuration) : IConfigureOptions<CacheOptions>
{
    private const string SectionName = "Caching:DefaultExpiration";

    /// <inheritdoc />
    public void Configure(CacheOptions options) => configuration.GetRequiredSection(SectionName).Bind(options);
}
