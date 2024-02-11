using Application.Features.UserOperationClaims.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using NArchitecture.Core.Security.Entities;

namespace Application.Features.UserOperationClaims.Rules;

public class UserOperationClaimBusinessRules : BaseBusinessRules
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly ILocalizationService _localizationService;

    public UserOperationClaimBusinessRules(
        IUserOperationClaimRepository userOperationClaimRepository,
        ILocalizationService localizationService
    )
    {
        _userOperationClaimRepository = userOperationClaimRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, UserOperationClaimsMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task UserOperationClaimShouldExistWhenSelected(UserOperationClaim<int, int>? userOperationClaim)
    {
        if (userOperationClaim == null)
            await throwBusinessException(UserOperationClaimsMessages.UserOperationClaimNotExists);
    }

    public async Task UserOperationClaimIdShouldExistWhenSelected(int id)
    {
        bool doesExist = await _userOperationClaimRepository.AnyAsync(predicate: b => b.Id == id);
        if (!doesExist)
            await throwBusinessException(UserOperationClaimsMessages.UserOperationClaimNotExists);
    }

    public async Task UserOperationClaimShouldNotExistWhenSelected(UserOperationClaim<int, int>? userOperationClaim)
    {
        if (userOperationClaim != null)
            await throwBusinessException(UserOperationClaimsMessages.UserOperationClaimAlreadyExists);
    }

    public async Task UserShouldNotHasOperationClaimAlreadyWhenInsert(int userId, int operationClaimId)
    {
        bool doesExist = await _userOperationClaimRepository.AnyAsync(u => u.UserId == userId && u.OperationClaimId == operationClaimId);
        if (doesExist)
            await throwBusinessException(UserOperationClaimsMessages.UserOperationClaimAlreadyExists);
    }

    public async Task UserShouldNotHasOperationClaimAlreadyWhenUpdated(int id, int userId, int operationClaimId)
    {
        bool doesExist = await _userOperationClaimRepository.AnyAsync(predicate: uoc =>
            uoc.Id == id && uoc.UserId == userId && uoc.OperationClaimId == operationClaimId
        );
        if (doesExist)
            await throwBusinessException(UserOperationClaimsMessages.UserOperationClaimAlreadyExists);
    }
}
