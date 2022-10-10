using Application.Features.FindeksCreditRates.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.FindeksCreditRates.Models;

public class FindeksCreditRateListModel : BasePageableModel
{
    public IList<FindeksCreditRateListDto> Items { get; set; }
}