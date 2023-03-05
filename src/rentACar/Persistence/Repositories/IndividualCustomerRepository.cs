using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class IndividualCustomerRepository : EfRepositoryBase<IndividualCustomer, BaseDbContext>, IIndividualCustomerRepository
{
    public IndividualCustomerRepository(BaseDbContext context)
        : base(context) { }
}
