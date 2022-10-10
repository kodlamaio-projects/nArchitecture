using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IRentalsAdditionalServiceRepository : IAsyncRepository<RentalsAdditionalService>,
                                                       IRepository<RentalsAdditionalService>
{
}