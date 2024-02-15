using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IUserOperationClaimRepository : IAsyncRepository<UserOperationClaim, Guid>, IRepository<UserOperationClaim, Guid>
{
    Task<IList<OperationClaim>> GetOperationClaimsByUserIdAsync(Guid userId);
}
