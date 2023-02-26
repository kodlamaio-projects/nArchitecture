using Application.Features.Brands.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Brands.Rules;

public class BrandBusinessRules : BaseBusinessRules
{
    private readonly IBrandRepository _brandRepository;

    public BrandBusinessRules(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task BrandIdShouldExistWhenSelected(int id)
    {
        Brand? result = await _brandRepository.GetAsync(b => b.Id == id, enableTracking: false);
        if (result == null) throw new BusinessException(BrandsMessages.BrandNotExists);
    }

    public async Task BrandNameCanNotBeDuplicatedWhenInserted(string name)
    {
        IPaginate<Brand> result = await _brandRepository.GetListAsync(b => b.Name == name, enableTracking: false);
        if (result.Items.Any()) throw new BusinessException(BrandsMessages.BrandNameExists);
    }
    public async Task BrandNameListCanNotBeDuplicatedWhenInserted(List<string> nameList)
    {
        IPaginate<Brand> result = await _brandRepository.GetListAsync(b => nameList.Contains(b.Name), enableTracking: false);
        if (result.Items.Any()) throw new BusinessException(BrandsMessages.BrandNameExists);
    }
}