using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories;

public interface IUserRepository : IAsyncRepository<User<int, int>, int>, IRepository<User<int, int>, int> { }
