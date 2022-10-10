using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class RentalBranchRepository : EfRepositoryBase<RentalBranch, BaseDbContext>, IRentalBranchRepository
{
    public RentalBranchRepository(BaseDbContext context) : base(context)
    {
    }
}