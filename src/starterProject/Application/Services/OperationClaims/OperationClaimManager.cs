using System.Linq.Expressions;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;
using NArchitecture.Core.Security.Entities;

namespace Application.Services.OperationClaims;

public class OperationClaimManager : IOperationClaimService
{
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

    public OperationClaimManager(
        IOperationClaimRepository operationClaimRepository,
        OperationClaimBusinessRules operationClaimBusinessRules
    )
    {
        _operationClaimRepository = operationClaimRepository;
        _operationClaimBusinessRules = operationClaimBusinessRules;
    }

    public async Task<OperationClaim<int, int>?> GetAsync(
        Expression<Func<OperationClaim<int, int>, bool>> predicate,
        Func<IQueryable<OperationClaim<int, int>>, IIncludableQueryable<OperationClaim<int, int>, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        OperationClaim<int, int>? operationClaim = await _operationClaimRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return operationClaim;
    }

    public async Task<IPaginate<OperationClaim<int, int>>?> GetListAsync(
        Expression<Func<OperationClaim<int, int>, bool>>? predicate = null,
        Func<IQueryable<OperationClaim<int, int>>, IOrderedQueryable<OperationClaim<int, int>>>? orderBy = null,
        Func<IQueryable<OperationClaim<int, int>>, IIncludableQueryable<OperationClaim<int, int>, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<OperationClaim<int, int>> operationClaimList = await _operationClaimRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return operationClaimList;
    }

    public async Task<OperationClaim<int, int>> AddAsync(OperationClaim<int, int> operationClaim)
    {
        await _operationClaimBusinessRules.OperationClaimNameShouldNotExistWhenCreating(operationClaim.Name);

        OperationClaim<int, int> addedOperationClaim = await _operationClaimRepository.AddAsync(operationClaim);

        return addedOperationClaim;
    }

    public async Task<OperationClaim<int, int>> UpdateAsync(OperationClaim<int, int> operationClaim)
    {
        await _operationClaimBusinessRules.OperationClaimNameShouldNotExistWhenUpdating(operationClaim.Id, operationClaim.Name);

        OperationClaim<int, int> updatedOperationClaim = await _operationClaimRepository.UpdateAsync(operationClaim);

        return updatedOperationClaim;
    }

    public async Task<OperationClaim<int, int>> DeleteAsync(OperationClaim<int, int> operationClaim, bool permanent = false)
    {
        OperationClaim<int, int> deletedOperationClaim = await _operationClaimRepository.DeleteAsync(operationClaim);

        return deletedOperationClaim;
    }
}
