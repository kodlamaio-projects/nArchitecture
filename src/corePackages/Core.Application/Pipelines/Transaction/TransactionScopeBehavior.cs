using MediatR;
using System.Transactions;

namespace Core.Application.Pipelines.Transaction
{
    public class TransactionScopeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            using (TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled))
            {
                TResponse response;
                try
                {
                    response = await next();
                    transactionScope.Complete();
                }
                catch (Exception)
                {
                    transactionScope.Dispose();
                    throw;
                }

                return response;
            }
        }
    }
}
