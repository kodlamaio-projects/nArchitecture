using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.Application.Pipelines.Logging;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ILoggableRequest
{
    private readonly LoggerServiceBase _loggerServiceBase;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public LoggingBehavior(LoggerServiceBase loggerServiceBase, IHttpContextAccessor httpContextAccessor)
    {
        _loggerServiceBase = loggerServiceBase;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
                                  RequestHandlerDelegate<TResponse> next)
    {
        List<LogParameter> logParameters = new();
        logParameters.Add(new LogParameter
        {
            Type = request.GetType().Name,
            Value = request
        });

        LogDetail logDetail = new()
        {
            MethodName = next.Method.Name,
            Parameters = logParameters,
            User = _httpContextAccessor.HttpContext == null ||
                   _httpContextAccessor.HttpContext.User.Identity.Name == null
                       ? "?"
                       : _httpContextAccessor.HttpContext.User.Identity.Name
        };

        _loggerServiceBase.Info(JsonConvert.SerializeObject(logDetail));

        return next();
    }
}