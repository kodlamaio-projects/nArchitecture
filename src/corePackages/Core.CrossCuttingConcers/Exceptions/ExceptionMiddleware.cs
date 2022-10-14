using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Core.CrossCuttingConcerns.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly LoggerServiceBase _loggerServiceBase;

    public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor contextAccessor, LoggerServiceBase loggerServiceBase)
    {
        _next = next;
        _contextAccessor = contextAccessor;
        _loggerServiceBase = loggerServiceBase;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await ExceptionLogging(context, exception);
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        if (exception.GetType() == typeof(ValidationException)) return CreateValidationException(context, exception);
        else if (exception.GetType() == typeof(BusinessException)) return CreateBusinessException(context, exception);
        else if (exception.GetType() == typeof(NotFoundException)) return CreateNotFoundException(context, exception);
        else if (exception.GetType() == typeof(AuthorizationException))
            return CreateAuthorizationException(context, exception);
        return CreateInternalException(context, exception);
    }

    private Task CreateAuthorizationException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.Unauthorized);

        return context.Response.WriteAsync(new AuthorizationProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Type = "https://example.com/probs/authorization",
            Title = "Authorization exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }

    private Task CreateBusinessException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);

        return context.Response.WriteAsync(new BusinessProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://example.com/probs/business",
            Title = "Business exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }

    private Task CreateValidationException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
        object errors = ((ValidationException)exception).Errors;

        return context.Response.WriteAsync(new ValidationProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://example.com/probs/validation",
            Title = "Validation error(s)",
            Detail = "",
            Instance = "",
            Errors = errors
        }.ToString());
    }

    private Task CreateNotFoundException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);

        return context.Response.WriteAsync(new NotFoundProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Type = "https://example.com/probs/notfound",
            Title = "Not Found exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }

    private Task CreateInternalException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);

        return context.Response.WriteAsync(new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://example.com/probs/internal",
            Title = "Internal exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }
    private Task ExceptionLogging(HttpContext context, Exception exception)
    {
        List<LogParameter> logParameters = new()
        {
            new LogParameter
            {
                Type = context.GetType().Name,
                Value = context
            }
        };

        LogDetail logDetail = new()
        {
            MethodName = _next.Method.Name,
            Parameters = logParameters,
            User = _contextAccessor.HttpContext == null ||
                   _contextAccessor.HttpContext.User.Identity.Name == null
            ? "?"
                       : _contextAccessor.HttpContext.User.Identity.Name
        };

        _loggerServiceBase.Info(JsonConvert.SerializeObject(logDetail));
        return Task.CompletedTask;
    }
}