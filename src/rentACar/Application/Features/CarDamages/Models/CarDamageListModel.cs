using Application.Features.CarDamages.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.CarDamages.Models;

public class CarDamageListModel : BasePageableModel
{
    public IList<CarDamageListDto> Items { get; set; }
}
