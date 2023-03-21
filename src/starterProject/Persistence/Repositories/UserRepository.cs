using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class UserRepository : EfRepositoryBase<User, BaseDbContext>, IUserRepository
{
    public UserRepository(BaseDbContext context)
        : base(context) { }
}
