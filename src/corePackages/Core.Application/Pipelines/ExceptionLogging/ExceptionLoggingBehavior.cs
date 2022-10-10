using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.Application.Pipelines.ExceptionLogging
{
    public class ExceptionLoggingBehavior<TRequest, TResponse> : BasePipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ExceptionLoggingBehavior(LoggerServiceBase loggerServiceBase, IHttpContextAccessor httpContextAccessor)
        {
            _loggerServiceBase = loggerServiceBase;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void Onexception(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next, Exception e)
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
                           ? "<anonymous>"
                           : _httpContextAccessor.HttpContext.User.Identity.Name
            };

            _loggerServiceBase.Error(JsonConvert.SerializeObject(logDetail));
        }
    }
}
