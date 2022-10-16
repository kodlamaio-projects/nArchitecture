using Core.Domain.Security.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IUserRepository : IAsyncRepository<User>, IRepository<User>
{
}