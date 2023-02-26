using Core.Application.Dtos;

namespace Application.Features.FindeksCreditRates.Queries.GetListFindeksCreditRate;

public class GetListFindeksCreditRateListItemDto : IDto
{
    public int Id { get; set; }
    public int Score { get; set; }
}