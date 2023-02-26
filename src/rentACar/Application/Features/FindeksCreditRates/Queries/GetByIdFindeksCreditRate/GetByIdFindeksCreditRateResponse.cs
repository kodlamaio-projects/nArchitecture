using Core.Application.Dtos;

namespace Application.Features.FindeksCreditRates.Queries.GetByIdFindeksCreditRate;

public class GetByIdFindeksCreditRateResponse : IDto
{
    public int Id { get; set; }
    public int Score { get; set; }
}