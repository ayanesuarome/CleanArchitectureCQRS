using CleanArch.Application.Abstractions.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace CleanArch.Infrastructure.Caching;

internal sealed class CacheService(IMemoryCache memoryCache) : ICacheService
{
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(5);

    public async Task<T> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<T>> factory,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default)
    {
        return await memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(expiration ?? DefaultExpiration);
                return factory(cancellationToken);
            });
    }
}
