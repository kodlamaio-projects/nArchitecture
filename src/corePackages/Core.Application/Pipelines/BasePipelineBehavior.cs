using MediatR;

namespace Core.Application.Pipelines;

public class BasePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected virtual void OnBefore(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) { }
    protected virtual void OnAfter(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) { }
    protected virtual void OnSuccess(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) { }
    protected virtual void Onexception(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next, Exception e) { }

    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        TResponse response = default;
        bool isSuccess = true;
        OnBefore(request, cancellationToken, next);
        try
        {
            response = await next();
        }
        catch (Exception e)
        {
            isSuccess = false;
            Onexception(request, cancellationToken, next, e);
            throw;
        }
        if (isSuccess)
            OnSuccess(request, cancellationToken, next);

        OnAfter(request, cancellationToken, next);

        return response;
    }
}
