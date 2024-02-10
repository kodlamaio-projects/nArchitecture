using System.Linq.Expressions;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.UsersService;

public class UserManager : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserBusinessRules _userBusinessRules;

    public UserManager(IUserRepository userRepository, UserBusinessRules userBusinessRules)
    {
        _userRepository = userRepository;
        _userBusinessRules = userBusinessRules;
    }

    public async Task<User<int, int>?> GetAsync(
        Expression<Func<User<int, int>, bool>> predicate,
        Func<IQueryable<User<int, int>>, IIncludableQueryable<User<int, int>, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        User<int, int>? user = await _userRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return user;
    }

    public async Task<IPaginate<User<int, int>>?> GetListAsync(
        Expression<Func<User<int, int>, bool>>? predicate = null,
        Func<IQueryable<User<int, int>>, IOrderedQueryable<User<int, int>>>? orderBy = null,
        Func<IQueryable<User<int, int>>, IIncludableQueryable<User<int, int>, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<User<int, int>> userList = await _userRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return userList;
    }

    public async Task<User<int, int>> AddAsync(User<int, int> user)
    {
        await _userBusinessRules.UserEmailShouldNotExistsWhenInsert(user.Email);

        User<int, int> addedUser = await _userRepository.AddAsync(user);

        return addedUser;
    }

    public async Task<User<int, int>> UpdateAsync(User<int, int> user)
    {
        await _userBusinessRules.UserEmailShouldNotExistsWhenUpdate(user.Id, user.Email);

        User<int, int> updatedUser = await _userRepository.UpdateAsync(user);

        return updatedUser;
    }

    public async Task<User<int, int>> DeleteAsync(User<int, int> user, bool permanent = false)
    {
        User<int, int> deletedUser = await _userRepository.DeleteAsync(user);

        return deletedUser;
    }
}
