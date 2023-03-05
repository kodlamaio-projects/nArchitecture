using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IRentalBranchRepository : IAsyncRepository<RentalBranch>, IRepository<RentalBranch> { }
