using Application.Features.RentalBranches.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.RentalBranches.Rules;

public class RentalBranchBusinessRules : BaseBusinessRules
{
    private readonly IRentalBranchRepository _rentalBranchRepository;

    public RentalBranchBusinessRules(IRentalBranchRepository rentalBranchRepository)
    {
        _rentalBranchRepository = rentalBranchRepository;
    }

    public async Task RentalBranchIdShouldExistWhenSelected(int id)
    {
        RentalBranch? result = await _rentalBranchRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException(RentalBranchMessages.RentalBranchNotExists);
    }
}