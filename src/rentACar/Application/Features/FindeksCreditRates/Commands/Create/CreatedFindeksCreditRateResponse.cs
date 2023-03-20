using Core.Application.Responses;

namespace Application.Features.FindeksCreditRates.Commands.Create;

public class CreatedFindeksCreditRateResponse : IResponse
{
    public int Id { get; set; }
    public int Score { get; set; }
}
