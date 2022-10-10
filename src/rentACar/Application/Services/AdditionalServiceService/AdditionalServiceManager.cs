using Application.Services.Repositories;
using Domain.Entities;

namespace Application.Services.AdditionalServiceService;

public class AdditionalServiceManager : IAdditionalServiceService
{
    private readonly IAdditionalServiceRepository _additionalServiceRepository;

    public AdditionalServiceManager(IAdditionalServiceRepository additionalServiceRepository)
    {
        _additionalServiceRepository = additionalServiceRepository;
    }

    public async Task<IList<AdditionalService>> GetListByIds(int[] ids)
    {
        IList<AdditionalService> additionalServices =
            (await _additionalServiceRepository.GetListAsync(a => ids.Contains(a.Id))).Items;

        return additionalServices;
    }
}