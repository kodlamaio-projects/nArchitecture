using NArchitecture.Core.Persistence.Repositories;
using NArchitecture.Core.Security.Entities;

namespace Application.Services.Repositories;

public interface IUserRepository : IAsyncRepository<User<int, int>, int>, IRepository<User<int, int>, int> { }
