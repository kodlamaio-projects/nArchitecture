using Core.Application.Responses;

namespace Application.Features.FindeksCreditRates.Queries.GetByIdFindeksCreditRate;

public class GetByIdFindeksCreditRateResponse : IResponse
{
    public int Id { get; set; }
    public int Score { get; set; }
}
