using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CorporateCustomerRepository : EfRepositoryBase<CorporateCustomer, BaseDbContext>,
                                           ICorporateCustomerRepository
{
    public CorporateCustomerRepository(BaseDbContext context) : base(context)
    {
    }
}