using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IFindeksCreditRateRepository : IAsyncRepository<FindeksCreditRate>,
                                                IRepository<FindeksCreditRate>
{
}