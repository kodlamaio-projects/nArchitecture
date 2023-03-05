using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IAdditionalServiceRepository : IAsyncRepository<AdditionalService>, IRepository<AdditionalService> { }
