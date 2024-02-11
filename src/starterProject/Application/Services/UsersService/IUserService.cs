using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;
using NArchitecture.Core.Security.Entities;

namespace Application.Services.UsersService;

public interface IUserService
{
    Task<User<int, int>?> GetAsync(
        Expression<Func<User<int, int>, bool>> predicate,
        Func<IQueryable<User<int, int>>, IIncludableQueryable<User<int, int>, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<IPaginate<User<int, int>>?> GetListAsync(
        Expression<Func<User<int, int>, bool>>? predicate = null,
        Func<IQueryable<User<int, int>>, IOrderedQueryable<User<int, int>>>? orderBy = null,
        Func<IQueryable<User<int, int>>, IIncludableQueryable<User<int, int>, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<User<int, int>> AddAsync(User<int, int> user);
    Task<User<int, int>> UpdateAsync(User<int, int> user);
    Task<User<int, int>> DeleteAsync(User<int, int> user, bool permanent = false);
}
