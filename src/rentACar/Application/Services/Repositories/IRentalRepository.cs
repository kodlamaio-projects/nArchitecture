using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IRentalRepository : IAsyncRepository<Rental>, IRepository<Rental> { }
