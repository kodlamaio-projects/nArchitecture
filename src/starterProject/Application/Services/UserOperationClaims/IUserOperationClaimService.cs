using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;
using NArchitecture.Core.Security.Entities;

namespace Application.Services.UserOperationClaims;

public interface IUserOperationClaimService
{
    Task<UserOperationClaim<int, int>?> GetAsync(
        Expression<Func<UserOperationClaim<int, int>, bool>> predicate,
        Func<IQueryable<UserOperationClaim<int, int>>, IIncludableQueryable<UserOperationClaim<int, int>, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<IPaginate<UserOperationClaim<int, int>>?> GetListAsync(
        Expression<Func<UserOperationClaim<int, int>, bool>>? predicate = null,
        Func<IQueryable<UserOperationClaim<int, int>>, IOrderedQueryable<UserOperationClaim<int, int>>>? orderBy = null,
        Func<IQueryable<UserOperationClaim<int, int>>, IIncludableQueryable<UserOperationClaim<int, int>, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<UserOperationClaim<int, int>> AddAsync(UserOperationClaim<int, int> userOperationClaim);
    Task<UserOperationClaim<int, int>> UpdateAsync(UserOperationClaim<int, int> userOperationClaim);
    Task<UserOperationClaim<int, int>> DeleteAsync(UserOperationClaim<int, int> userOperationClaim, bool permanent = false);
}
