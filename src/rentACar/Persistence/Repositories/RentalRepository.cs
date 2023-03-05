using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class RentalRepository : EfRepositoryBase<Rental, BaseDbContext>, IRentalRepository
{
    public RentalRepository(BaseDbContext context)
        : base(context) { }
}
