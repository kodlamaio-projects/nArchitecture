using Application.Features.UserOperationClaims.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;

namespace Application.Features.UserOperationClaims.Rules;

public class UserOperationClaimBusinessRules
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

    public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
    }

    public async Task UserOperationClaimIdShouldExistWhenSelected(int id)
    {
        UserOperationClaim? result = await _userOperationClaimRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException(UserOperationClaimMessages.UserOperationClaimNotExists);
    }

    public async Task ThisOperationClaimCannotBeDuplicatedForThisUserWhenCreated(int userId, int operationCliamId)
    {
        UserOperationClaim? result = await _userOperationClaimRepository.GetAsync(uoc => uoc.UserId == userId && uoc.OperationClaimId == operationCliamId);
        if (result != null) throw new BusinessException(UserOperationClaimMessages.ThisUserAlreadyHasThisOperationClaim);
    }

    public async Task ThisOperationClaimCannotBeDuplicatedForThisUserWhenUpdated(int id, int userId, int operationCliamId)
    {
        UserOperationClaim? result = await _userOperationClaimRepository.GetAsync(uoc => uoc.Id != id && uoc.UserId == userId && uoc.OperationClaimId == operationCliamId);
        if (result != null) throw new BusinessException(UserOperationClaimMessages.ThisUserAlreadyHasThisOperationClaim);
    }
}