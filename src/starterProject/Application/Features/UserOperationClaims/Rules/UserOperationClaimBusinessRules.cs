using Application.Features.UserOperationClaims.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;

namespace Application.Features.UserOperationClaims.Rules;

public class UserOperationClaimBusinessRules : BaseBusinessRules
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

    public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
    }

    public Task UserOperationClaimShouldExistWhenSelected(UserOperationClaim? userOperationClaim)
    {
        if (userOperationClaim == null)
            throw new BusinessException(UserOperationClaimsMessages.UserOperationClaimNotExists);
        return Task.CompletedTask;
    }

    public async Task UserOperationClaimIdShouldExistWhenSelected(int id)
    {
        bool doesExist = await _userOperationClaimRepository.AnyAsync(predicate: b => b.Id == id);
        if (!doesExist)
            throw new BusinessException(UserOperationClaimsMessages.UserOperationClaimNotExists);
    }

    public Task UserOperationClaimShouldNotExistWhenSelected(UserOperationClaim? userOperationClaim)
    {
        if (userOperationClaim != null)
            throw new BusinessException(UserOperationClaimsMessages.UserOperationClaimAlreadyExists);
        return Task.CompletedTask;
    }

    public async Task UserShouldNotHasOperationClaimAlreadyWhenInsert(int userId, int operationClaimId)
    {
        bool doesExist = await _userOperationClaimRepository.AnyAsync(u => u.UserId == userId && u.OperationClaimId == operationClaimId);
        if (doesExist)
            throw new BusinessException(UserOperationClaimsMessages.UserOperationClaimAlreadyExists);
    }

    public async Task UserShouldNotHasOperationClaimAlreadyWhenUpdated(int id, int userId, int operationClaimId)
    {
        bool doesExist = await _userOperationClaimRepository.AnyAsync(
            predicate: uoc => uoc.Id == id && uoc.UserId == userId && uoc.OperationClaimId == operationClaimId
        );
        if (doesExist)
            throw new BusinessException(UserOperationClaimsMessages.UserOperationClaimAlreadyExists);
    }
}
