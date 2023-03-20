using Core.Application.Responses;

namespace Application.Features.FindeksCreditRates.Commands.UpdateByUserIdFromService;

public class UpdateByUserIdFindeksCreditRateFromServiceResponse : IResponse
{
    public int Id { get; set; }
    public int Score { get; set; }
}
