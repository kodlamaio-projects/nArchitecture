using NArchitecture.Core.Persistence.Repositories;
using NArchitecture.Core.Security.Entities;

namespace Application.Services.Repositories;

public interface IOperationClaimRepository : IAsyncRepository<OperationClaim<int, int>, int>, IRepository<OperationClaim<int, int>, int> { }
