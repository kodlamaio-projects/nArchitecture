using Application.Features.Rentals.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.Rentals.Models;

public class RentalListModel : BasePageableModel
{
    public IList<RentalListDto> Items { get; set; }
}