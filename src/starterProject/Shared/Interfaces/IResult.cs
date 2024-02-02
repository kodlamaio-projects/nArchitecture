namespace Shared.Interfaces;

public interface IResult
{
    bool IsSuccess { get; }
    string? Message { get; }
    IEnumerable<string>? Messages { get; }
    int? StatusCode { get; }

}

public interface IResult<T> : IResult
{
    T? Value { get; }
    IEnumerable<T>? Values { get; }

}
