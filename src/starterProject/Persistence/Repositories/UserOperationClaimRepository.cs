using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
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
        var operationClaims = await Query()
            .AsNoTracking()
            .Where(p => p.UserId == userId)
            .Select(p => new OperationClaim<int, int> { Id = p.OperationClaimId, Name = p.OperationClaim.Name })
            .ToListAsync();
        return operationClaims;
    }
}
