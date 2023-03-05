using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Pipelines.Performance;

public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IIntervalRequest
{
    private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;
    private readonly Stopwatch _stopwatch;

    public PerformanceBehavior(ILogger<PerformanceBehavior<TRequest, TResponse>> logger, Stopwatch stopwatch)
    {
        _logger = logger;
        _stopwatch = stopwatch;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = request.GetType().Name;

        TResponse response;

        try
        {
            _stopwatch.Start();
            response = await next();
        }
        finally
        {
            if (_stopwatch.Elapsed.TotalSeconds > request.Interval)
            {
                string message = $"Performance -> {requestName} {_stopwatch.Elapsed.TotalSeconds} s";

                Debug.WriteLine(message);
                _logger.LogInformation(message);
            }

            _stopwatch.Restart();
        }

        return response;
    }
}
