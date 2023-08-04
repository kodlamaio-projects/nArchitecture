using Application.Features.OperationClaims.Constants;
using Application.Services.Repositories;
using Application.Services.TranslateService;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;

namespace Application.Features.OperationClaims.Rules;

public class OperationClaimBusinessRules : BaseBusinessRules
{
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly ITranslateService _translateService;

    public OperationClaimBusinessRules(
        IOperationClaimRepository operationClaimRepository,
        ITranslateService translateService
        )
    {
        _operationClaimRepository = operationClaimRepository;
        _translateService = translateService;
    }

    public override async Task ThrowBusinessException(string message)
    {
        string translatedMessage = await _translateService.TranslateAsync(message);
        await base.ThrowBusinessException(translatedMessage);
    }
    public async Task OperationClaimShouldExistWhenSelected(OperationClaim? operationClaim)
    {
        if (operationClaim == null)
            await ThrowBusinessException(OperationClaimsMessages.NotExists);
    }

    public async Task OperationClaimIdShouldExistWhenSelected(int id)
    {
        bool doesExist = await _operationClaimRepository.AnyAsync(predicate: b => b.Id == id, enableTracking: false);
        if (doesExist)
            await ThrowBusinessException(OperationClaimsMessages.NotExists);
    }

    public async Task OperationClaimNameShouldNotExistWhenCreating(string name)
    {
        bool doesExist = await _operationClaimRepository.AnyAsync(predicate: b => b.Name == name, enableTracking: false);
        if (doesExist)
            await ThrowBusinessException(OperationClaimsMessages.AlreadyExists);
    }

    public async Task OperationClaimNameShouldNotExistWhenUpdating(int id, string name)
    {
        bool doesExist = await _operationClaimRepository.AnyAsync(predicate: b => b.Id != id && b.Name == name, enableTracking: false);
        if (doesExist)
            await ThrowBusinessException(OperationClaimsMessages.AlreadyExists);
    }
}
