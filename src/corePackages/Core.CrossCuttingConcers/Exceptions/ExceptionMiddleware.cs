using Core.CrossCuttingConcerns.Enums;
using Core.CrossCuttingConcerns.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Core.CrossCuttingConcerns.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        return exception switch
        {
            ValidationException => CreateValidationException(context, exception),
            BusinessException => CreateBusinessException(context, exception),
            AuthorizationException => CreateAuthorizationException(context, exception),
            _ => CreateInternalException(context, exception),
        };
    }

    private Task CreateAuthorizationException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = HttpStatusCode.Unauthorized.GetCode();

        return context.Response.WriteAsync(new AuthorizationProblemDetails
        {
            Status = context.Response.StatusCode,
            Type = "https://example.com/probs/authorization",
            Title = "Authorization exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }

    private Task CreateBusinessException(HttpContext context, Exception exception)
    {
        if (exception is BusinessException businessException)
            context.Response.StatusCode = businessException.BusinessExceptionType == BusinessExceptionTypes.NotFound ? HttpStatusCode.NotFound.GetCode() : HttpStatusCode.BadRequest.GetCode();

        return context.Response.WriteAsync(new BusinessProblemDetails
        {
            Status = context.Response.StatusCode,
            Type = "https://example.com/probs/business",
            Title = "Business exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }

    private Task CreateValidationException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = HttpStatusCode.BadRequest.GetCode();
        object errors = ((ValidationException)exception).Errors;

        return context.Response.WriteAsync(new ValidationProblemDetails
        {
            Status = context.Response.StatusCode,
            Type = "https://example.com/probs/validation",
            Title = "Validation error(s)",
            Detail = "",
            Instance = "",
            Errors = errors
        }.ToString());
    }

    private Task CreateInternalException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = HttpStatusCode.InternalServerError.GetCode();

        return context.Response.WriteAsync(new InternalServerProblemDetails
        {
            Status = context.Response.StatusCode,
            Type = "https://example.com/probs/internal",
            Title = "Internal exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }
}