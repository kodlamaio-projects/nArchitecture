using Application.Features.UserOperationClaims.Constants;
using Application.Services.Repositories;
using Application.Services.TranslateService;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;

namespace Application.Features.UserOperationClaims.Rules;

public class UserOperationClaimBusinessRules : BaseBusinessRules
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly ITranslateService _translateService;

    public UserOperationClaimBusinessRules(
        IUserOperationClaimRepository userOperationClaimRepository,
        ITranslateService translateService
        )
    {
        _userOperationClaimRepository = userOperationClaimRepository;
        _translateService = translateService;
    }

    public override async Task ThrowBusinessException(string message)
    {
        string translatedMessage = await _translateService.TranslateAsync(message);
        await base.ThrowBusinessException(translatedMessage);
    }

    public async Task UserOperationClaimShouldExistWhenSelected(UserOperationClaim? userOperationClaim)
    {
        if (userOperationClaim == null)
            await ThrowBusinessException(UserOperationClaimsMessages.UserOperationClaimNotExists);
    }

    public async Task UserOperationClaimIdShouldExistWhenSelected(int id)
    {
        bool doesExist = await _userOperationClaimRepository.AnyAsync(predicate: b => b.Id == id);
        if (!doesExist)
            await ThrowBusinessException(UserOperationClaimsMessages.UserOperationClaimNotExists);
    }

    public async Task UserOperationClaimShouldNotExistWhenSelected(UserOperationClaim? userOperationClaim)
    {
        if (userOperationClaim != null)
            await ThrowBusinessException(UserOperationClaimsMessages.UserOperationClaimAlreadyExists);
    }

    public async Task UserShouldNotHasOperationClaimAlreadyWhenInsert(int userId, int operationClaimId)
    {
        bool doesExist = await _userOperationClaimRepository.AnyAsync(u => u.UserId == userId && u.OperationClaimId == operationClaimId);
        if (doesExist)
            await ThrowBusinessException(UserOperationClaimsMessages.UserOperationClaimAlreadyExists);
    }

    public async Task UserShouldNotHasOperationClaimAlreadyWhenUpdated(int id, int userId, int operationClaimId)
    {
        bool doesExist = await _userOperationClaimRepository.AnyAsync(
            predicate: uoc => uoc.Id == id && uoc.UserId == userId && uoc.OperationClaimId == operationClaimId
        );
        if (doesExist)
            await ThrowBusinessException(UserOperationClaimsMessages.UserOperationClaimAlreadyExists);
    }
}
