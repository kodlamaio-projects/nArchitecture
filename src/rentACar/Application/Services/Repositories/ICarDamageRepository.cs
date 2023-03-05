using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface ICarDamageRepository : IAsyncRepository<CarDamage>, IRepository<CarDamage> { }
