using Application.Features.RentalBranches.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.RentalBranches.Rules;

public class RentalBranchBusinessRules
{
    private readonly IRentalBranchRepository _rentalBranchRepository;

    public RentalBranchBusinessRules(IRentalBranchRepository rentalBranchRepository)
    {
        _rentalBranchRepository = rentalBranchRepository;
    }

    public async Task RentalBranchIdShouldExistWhenSelected(int id)
    {
        RentalBranch? result = await _rentalBranchRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException(RentalBranchExceptionMessage.RentalBranchNotExistsMessage);
    }
}