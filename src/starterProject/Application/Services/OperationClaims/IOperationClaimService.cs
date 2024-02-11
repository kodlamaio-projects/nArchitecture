using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;
using NArchitecture.Core.Security.Entities;

namespace Application.Services.OperationClaims;

public interface IOperationClaimService
{
    Task<OperationClaim<int, int>?> GetAsync(
        Expression<Func<OperationClaim<int, int>, bool>> predicate,
        Func<IQueryable<OperationClaim<int, int>>, IIncludableQueryable<OperationClaim<int, int>, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<IPaginate<OperationClaim<int, int>>?> GetListAsync(
        Expression<Func<OperationClaim<int, int>, bool>>? predicate = null,
        Func<IQueryable<OperationClaim<int, int>>, IOrderedQueryable<OperationClaim<int, int>>>? orderBy = null,
        Func<IQueryable<OperationClaim<int, int>>, IIncludableQueryable<OperationClaim<int, int>, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<OperationClaim<int, int>> AddAsync(OperationClaim<int, int> operationClaim);
    Task<OperationClaim<int, int>> UpdateAsync(OperationClaim<int, int> operationClaim);
    Task<OperationClaim<int, int>> DeleteAsync(OperationClaim<int, int> operationClaim, bool permanent = false);
}
