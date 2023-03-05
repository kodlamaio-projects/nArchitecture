using Domain.Entities;

namespace Application.Services.AdditionalServiceService;

public interface IAdditionalServiceService
{
    public Task<IList<AdditionalService>> GetListByIds(int[] ids);
}
