using Shared.Interfaces;

namespace Shared.Implementations;

public class Result : IResult
{
    public bool IsSuccess { get; private set; }

    public string? Message { get; private set; }

    public IEnumerable<string>? Messages { get; private set; }

    public int? StatusCode { get; private set; }

    public static Result Success(int? statusCode = null, string? message = null)
    {
        return new Result
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Message = message
        };
    }

    public static Result Failure(int? statusCode = null, string? error = null, IEnumerable<string>? errors = null)
    {
        return new Result
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = error,
            Messages = errors
        };
    }
}

public class Result<T> : IResult<T>
{
    public T? Value { get; private set; }

    public IEnumerable<T>? Values { get; private set; }

    public bool IsSuccess { get; private set; }

    public string? Message { get; private set; }

    public IEnumerable<string>? Messages { get; private set; }

    public int? StatusCode { get; private set; }

    public static Result<T> Success(int? statusCode = null, string? message = null, IEnumerable<string>?messages = null)
    {
        return new Result<T>
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Message = message,
            Messages = messages
        };
    }

    public static Result<T> Success(T value, int? statusCode = null, string? message = null, IEnumerable<string>? messages = null)
    {
        return new Result<T>
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Message = message,
            Messages = messages,
            Value = value
        };
    }

    public static Result<T> Success(IEnumerable<T> values, int? statusCode = null, string? message = null, IEnumerable<string>? messages = null)
    {
        return new Result<T>
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Message = message,
            Messages = messages,
            Values = values
        };
    }



    public static Result<T> Failure(int? statusCode = null, string? error = null, IEnumerable<string>? errors = null)
    {
        return new Result<T>
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = error,
            Messages = errors
        };
    }



}
