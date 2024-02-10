using System.Linq.Expressions;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.UserOperationClaims;

public class UserUserOperationClaimManager : IUserOperationClaimService
{
    private readonly IUserOperationClaimRepository _userUserOperationClaimRepository;
    private readonly UserOperationClaimBusinessRules _userUserOperationClaimBusinessRules;

    public UserUserOperationClaimManager(
        IUserOperationClaimRepository userUserOperationClaimRepository,
        UserOperationClaimBusinessRules userUserOperationClaimBusinessRules
    )
    {
        _userUserOperationClaimRepository = userUserOperationClaimRepository;
        _userUserOperationClaimBusinessRules = userUserOperationClaimBusinessRules;
    }

    public async Task<UserOperationClaim<int, int>?> GetAsync(
        Expression<Func<UserOperationClaim<int, int>, bool>> predicate,
        Func<IQueryable<UserOperationClaim<int, int>>, IIncludableQueryable<UserOperationClaim<int, int>, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        UserOperationClaim<int, int>? userUserOperationClaim = await _userUserOperationClaimRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return userUserOperationClaim;
    }

    public async Task<IPaginate<UserOperationClaim<int, int>>?> GetListAsync(
        Expression<Func<UserOperationClaim<int, int>, bool>>? predicate = null,
        Func<IQueryable<UserOperationClaim<int, int>>, IOrderedQueryable<UserOperationClaim<int, int>>>? orderBy = null,
        Func<IQueryable<UserOperationClaim<int, int>>, IIncludableQueryable<UserOperationClaim<int, int>, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<UserOperationClaim<int, int>> userUserOperationClaimList = await _userUserOperationClaimRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return userUserOperationClaimList;
    }

    public async Task<UserOperationClaim<int, int>> AddAsync(UserOperationClaim<int, int> userUserOperationClaim)
    {
        await _userUserOperationClaimBusinessRules.UserShouldNotHasOperationClaimAlreadyWhenInsert(
            userUserOperationClaim.UserId,
            userUserOperationClaim.OperationClaimId
        );

        UserOperationClaim<int, int> addedUserOperationClaim = await _userUserOperationClaimRepository.AddAsync(userUserOperationClaim);

        return addedUserOperationClaim;
    }

    public async Task<UserOperationClaim<int, int>> UpdateAsync(UserOperationClaim<int, int> userUserOperationClaim)
    {
        await _userUserOperationClaimBusinessRules.UserShouldNotHasOperationClaimAlreadyWhenUpdated(
            userUserOperationClaim.Id,
            userUserOperationClaim.UserId,
            userUserOperationClaim.OperationClaimId
        );

        UserOperationClaim<int, int> updatedUserOperationClaim = await _userUserOperationClaimRepository.UpdateAsync(
            userUserOperationClaim
        );

        return updatedUserOperationClaim;
    }

    public async Task<UserOperationClaim<int, int>> DeleteAsync(UserOperationClaim<int, int> userUserOperationClaim, bool permanent = false)
    {
        UserOperationClaim<int, int> deletedUserOperationClaim = await _userUserOperationClaimRepository.DeleteAsync(
            userUserOperationClaim
        );

        return deletedUserOperationClaim;
    }
}
