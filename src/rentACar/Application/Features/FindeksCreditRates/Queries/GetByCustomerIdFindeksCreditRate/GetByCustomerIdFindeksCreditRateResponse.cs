using Core.Application.Responses;

namespace Application.Features.FindeksCreditRates.Queries.GetByCustomerIdFindeksCreditRate;

public class GetByCustomerIdFindeksCreditRateResponse : IResponse
{
    public int Id { get; set; }
    public int Score { get; set; }
}
