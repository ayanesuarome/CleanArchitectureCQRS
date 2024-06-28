using CleanArch.Application.Abstractions.Caching;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CleanArch.Infrastructure.Caching;

internal sealed class CacheService : ICacheService
{
    public CacheService(
        IMemoryCache memoryCache,
        IOptions<CacheOptions> options)
    {
        this._memoryCache = memoryCache;
        this._options = options;
        _defaultExpiration = TimeSpan.FromMinutes(5);
    }

    private readonly IMemoryCache _memoryCache;
    private readonly IOptions<CacheOptions> _options;
    private readonly TimeSpan _defaultExpiration;

    public async Task<T> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<T>> factory,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default)
    {
        return await _memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(expiration ?? _defaultExpiration);
                return factory(cancellationToken);
            });
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }
}
