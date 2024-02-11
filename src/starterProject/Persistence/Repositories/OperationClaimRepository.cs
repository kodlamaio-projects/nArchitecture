using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Repositories;
using NArchitecture.Core.Security.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OperationClaimRepository : EfRepositoryBase<OperationClaim<int, int>, int, BaseDbContext>, IOperationClaimRepository
{
    public OperationClaimRepository(BaseDbContext context)
        : base(context) { }
}
