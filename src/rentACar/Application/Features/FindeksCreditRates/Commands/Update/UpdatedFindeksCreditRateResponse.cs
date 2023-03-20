using Core.Application.Responses;

namespace Application.Features.FindeksCreditRates.Commands.Update;

public class UpdatedFindeksCreditRateResponse : IResponse
{
    public int Id { get; set; }
    public int Score { get; set; }
}
