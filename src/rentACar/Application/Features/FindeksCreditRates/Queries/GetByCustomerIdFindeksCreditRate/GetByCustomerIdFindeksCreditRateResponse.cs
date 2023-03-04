using Core.Application.Dtos;

namespace Application.Features.FindeksCreditRates.Queries.GetByCustomerIdFindeksCreditRate;

public class GetByCustomerIdFindeksCreditRateResponse : IDto
{
    public int Id { get; set; }
    public int Score { get; set; }
}