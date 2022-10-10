using Application.Features.AdditionalServices.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.AdditionalServices.Models;

public class AdditionalServiceListModel : BasePageableModel
{
    public IList<AdditionalServiceListDto> Items { get; set; }
}