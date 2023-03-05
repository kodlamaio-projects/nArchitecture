using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CarDamageRepository : EfRepositoryBase<CarDamage, BaseDbContext>, ICarDamageRepository
{
    public CarDamageRepository(BaseDbContext context)
        : base(context) { }
}
