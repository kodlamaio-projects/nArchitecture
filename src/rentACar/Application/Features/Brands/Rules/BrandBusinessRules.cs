using Application.Features.Brands.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace Application.Features.Brands.Rules;

public class BrandBusinessRules : BaseBusinessRules
{
    private readonly IBrandRepository _brandRepository;

    public BrandBusinessRules(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public void BrandIdShouldExistWhenSelected(Brand? brand)
    {
        if (brand == null) throw new BusinessException(BrandsMessages.BrandNotExists);
    }

    public async Task BrandIdShouldExistWhenSelected(int id)
    {
        Brand? brand = await _brandRepository.GetAsync(b => b.Id == id, enableTracking: false);
        BrandIdShouldExistWhenSelected(brand);
    }

    public async Task BrandNameCanNotBeDuplicatedWhenInserted(string name)
    {
        Brand? result = await _brandRepository.GetAsync(x => string.Equals(x.Name.ToLower(), name.ToLower(),
                                                                           StringComparison.Ordinal));
        if (result != null) throw new BusinessException(BrandsMessages.BrandNameExists);
    }

    public async Task BrandNameCanNotBeDuplicatedWhenUpdated(Brand brand)
    {
        Brand? result = await _brandRepository.GetAsync(x => (x.Id != brand.Id) && (string.Equals(x.Name.ToLower(),
                                                                         brand.Name.ToLower(),
                                                                         StringComparison.Ordinal)));
        if (result != null) throw new BusinessException(BrandsMessages.BrandNameExists);
    }

    public async Task BrandNameListCanNotBeDuplicatedWhenInserted(List<string> nameList)
    {
        IPaginate<Brand> result = await _brandRepository.GetListAsync(b => nameList.Contains(b.Name), enableTracking: false);
        if (result.Items.Any()) throw new BusinessException(BrandsMessages.BrandNameExists);
    }
}