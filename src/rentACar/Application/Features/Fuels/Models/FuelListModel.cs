using Application.Features.Fuels.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.Fuels.Models;

public class FuelListModel : BasePageableModel
{
    public IList<FuelListDto> Items { get; set; }
}