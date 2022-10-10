using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface ICorporateCustomerRepository : IAsyncRepository<CorporateCustomer>, IRepository<CorporateCustomer>
{
}