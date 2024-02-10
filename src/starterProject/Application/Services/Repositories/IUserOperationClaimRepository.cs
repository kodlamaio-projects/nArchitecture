using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories;

public interface IUserOperationClaimRepository
    : IAsyncRepository<UserOperationClaim<int, int>, int>,
        IRepository<UserOperationClaim<int, int>, int>
{
    Task<IList<OperationClaim<int, int>>> GetOperationClaimsByUserIdAsync(int userId);
}
