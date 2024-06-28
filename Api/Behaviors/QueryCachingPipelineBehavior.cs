using CleanArch.Application.Abstractions.Caching;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Core.Primitives.Result;
using MediatR;

namespace CleanArch.Api.Behaviors;

internal sealed class QueryCachingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedQuery
    where TResponse : Result
{
    private readonly ICacheService _cacheService;

    public QueryCachingPipelineBehavior(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        return await _cacheService.GetOrCreateAsync(
            request.CacheKey,
            _ => next(),
            request.Expiration,
            cancellationToken);
    }
}
