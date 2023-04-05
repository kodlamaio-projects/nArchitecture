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
        UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(
            predicate: b => b.Id == id,
            enableTracking: false
        );
        await UserOperationClaimShouldExistWhenSelected(userOperationClaim);
    }

    public async Task UserShouldNotHasOperationClaimIdWhenInsert(int userId, int operationClaimId)
    {
        bool isExist = await _userOperationClaimRepository.AnyAsync(u => u.UserId == userId && u.OperationClaimId == operationClaimId);
        if (isExist)
            throw new BusinessException(UserOperationClaimsMessages.UserOperationClaimAlreadyExists);
    }
}
