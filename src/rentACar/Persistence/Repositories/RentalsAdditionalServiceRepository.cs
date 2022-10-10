using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class RentalsAdditionalServiceRepository : EfRepositoryBase<RentalsAdditionalService, BaseDbContext>,
                                                  IRentalsAdditionalServiceRepository
{
    public RentalsAdditionalServiceRepository(BaseDbContext context) : base(context)
    {
    }
}