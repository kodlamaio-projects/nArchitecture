using System.Data.Entity;
using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CarRepository : EfRepositoryBase<Car, BaseDbContext>, ICarRepository
{
    public CarRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<IList<Car>> GetCarListByModelIdAndRentalBranchId(int modelId, int rentStartRentalBranch)
    {
        var result = from c in Context.Cars
            where c.ModelId == modelId && c.RentalBranchId == rentStartRentalBranch
            select c;

        return await result.ToListAsync();
    }
}