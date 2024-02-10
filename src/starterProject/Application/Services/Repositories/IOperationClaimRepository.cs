using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories;

public interface IOperationClaimRepository : IAsyncRepository<OperationClaim<int, int>, int>, IRepository<OperationClaim<int, int>, int> { }
