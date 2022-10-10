using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface ITransmissionRepository : IAsyncRepository<Transmission>, IRepository<Transmission>
{
}