using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IIndividualCustomerRepository : IAsyncRepository<IndividualCustomer>, IRepository<IndividualCustomer>
{
}