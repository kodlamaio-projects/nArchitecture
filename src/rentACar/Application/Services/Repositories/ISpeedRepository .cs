using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface ISpeedRepository : IAsyncRepository<Speed>, IRepository<Speed>
{
}