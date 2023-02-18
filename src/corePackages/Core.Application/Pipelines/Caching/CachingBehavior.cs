using System.Text;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Core.Application.Pipelines.Caching;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICachableRequest
{
    private readonly IDistributedCache _cache;

    private readonly CacheSettings _cacheSettings;
    private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;

    public CachingBehavior(IDistributedCache cache, ILogger<CachingBehavior<TRequest, TResponse>> logger,
        IConfiguration configuration)
    {
        _cache = cache;
        _logger = logger;
        _cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>();
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        TResponse response;
        if (request.BypassCache) return await next();

        async Task<TResponse> GetResponseAndAddToCache()
        {
            response = await next();
            TimeSpan? slidingExpiration =
                request.SlidingExpiration ?? TimeSpan.FromDays(_cacheSettings.SlidingExpiration);
            DistributedCacheEntryOptions cacheOptions = new() { SlidingExpiration = slidingExpiration };
            byte[] serializeData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response));
            await _cache.SetAsync(request.CacheKey, serializeData, cacheOptions, cancellationToken);
            return response;
        }

        byte[]? cachedResponse = await _cache.GetAsync(request.CacheKey, cancellationToken);
        if (cachedResponse != null)
        {
            response = JsonSerializer.Deserialize<TResponse>(Encoding.Default.GetString(cachedResponse));
            _logger.LogInformation($"Fetched from Cache -> {request.CacheKey}");
        }
        else
        {
            response = await GetResponseAndAddToCache();
            _logger.LogInformation($"Added to Cache -> {request.CacheKey}");
        }

        return response;
    }
}