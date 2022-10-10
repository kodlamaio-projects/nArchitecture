using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class SpeedRepository : EfRepositoryBase<Speed, BaseDbContext>, ISpeedRepository
{
    public SpeedRepository(BaseDbContext context) : base(context)
    {
    }
}