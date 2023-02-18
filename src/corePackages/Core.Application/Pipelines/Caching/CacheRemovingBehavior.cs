using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Core.Application.Pipelines.Caching;

public class CacheRemovingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICacheRemoverRequest
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<CacheRemovingBehavior<TRequest, TResponse>> _logger;

    public CacheRemovingBehavior(IDistributedCache cache, ILogger<CacheRemovingBehavior<TRequest, TResponse>> logger
    )
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        TResponse response;
        if (request.BypassCache) return await next();

        async Task<TResponse> GetResponseAndRemoveCache()
        {
            response = await next();
            await _cache.RemoveAsync(request.CacheKey, cancellationToken);
            return response;
        }

        response = await GetResponseAndRemoveCache();
        _logger.LogInformation($"Removed Cache -> {request.CacheKey}");

        return response;
    }
}