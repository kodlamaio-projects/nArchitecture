using Application.Features.Brands.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
        if (brand == null) throw new BusinessException(BrandMessages.BrandNotExists);
    }

    public async Task BrandNameCanNotBeDuplicatedWhenInserted(string name)
    {
        var result = await _brandRepository.Query().Where(x => x.Name == name).AnyAsync();
        if (result) throw new BusinessException(BrandMessages.BrandNameExists);
    }

    public async Task BrandNameCanNotBeDuplicatedWhenUpdated(int id, string name)
    {
        var result = await _brandRepository.Query().Where(x => x.Name == name).AnyAsync();
        if (result)
        {
            result = await _brandRepository.Query().Where(x => (x.Id == id && x.Name == name)).AnyAsync();

            if (!result)
                throw new BusinessException(BrandMessages.BrandNameExists);
        }
    }

    public async Task BrandNameListCanNotBeDuplicatedWhenInserted(List<string> nameList)
    {
        IPaginate<Brand> result = await _brandRepository.GetListAsync(b => nameList.Contains(b.Name));
        if (result.Items.Any()) throw new BusinessException(BrandMessages.BrandNameExists);
    }
}