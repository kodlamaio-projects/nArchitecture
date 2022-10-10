using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;

namespace Application.Features.OperationClaims.Rules;

public class OperationClaimBusinessRules
{
    private readonly IOperationClaimRepository _operationClaimRepository;

    public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
    {
        _operationClaimRepository = operationClaimRepository;
    }

    public async Task OperationClaimIdShouldExistWhenSelected(int id)
    {
        OperationClaim? result = await _operationClaimRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException("OperationClaim not exists.");
    }
}