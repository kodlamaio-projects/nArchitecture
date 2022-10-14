using Application.Features.Brands.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Brands.Rules;

public class BrandBusinessRules
{
    private readonly IBrandRepository _brandRepository;

    public BrandBusinessRules(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task BrandIdShouldExistWhenSelected(int id)
    {
        Brand? result = await _brandRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException(BrandMessages.BrandNotExists);
    }

    public async Task BrandNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<Brand> result = await _brandRepository.GetListAsync(b => b.Name == name);
        if (result.Items.Any()) throw new BusinessException(BrandMessages.BrandNameExists);
    }
}