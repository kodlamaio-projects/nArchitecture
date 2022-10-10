using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class FindeksCreditRateRepository : EfRepositoryBase<FindeksCreditRate, BaseDbContext>,
                                           IFindeksCreditRateRepository
{
    public FindeksCreditRateRepository(BaseDbContext context) : base(context)
    {
    }
}