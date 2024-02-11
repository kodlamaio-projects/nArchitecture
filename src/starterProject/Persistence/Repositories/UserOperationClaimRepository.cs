using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Persistence.Repositories;
using NArchitecture.Core.Security.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class UserOperationClaimRepository
    : EfRepositoryBase<UserOperationClaim<int, int>, int, BaseDbContext>,
        IUserOperationClaimRepository
{
    public UserOperationClaimRepository(BaseDbContext context)
        : base(context) { }

    public async Task<IList<OperationClaim<int, int>>> GetOperationClaimsByUserIdAsync(int userId)
    {
        List<OperationClaim<int, int>> operationClaims = await Query()
            .AsNoTracking()
            .Where(p => p.UserId == userId)
            .Select(p => new OperationClaim<int, int> { Id = p.OperationClaimId, Name = p.OperationClaim.Name })
            .ToListAsync();
        return operationClaims;
    }
}
