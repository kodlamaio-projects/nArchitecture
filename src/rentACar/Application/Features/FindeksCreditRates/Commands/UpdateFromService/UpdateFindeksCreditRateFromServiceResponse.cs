using Core.Application.Responses;

namespace Application.Features.FindeksCreditRates.Commands.UpdateFromService;

public class UpdateFindeksCreditRateFromServiceResponse : IResponse
{
    public int Id { get; set; }
    public int Score { get; set; }
}
