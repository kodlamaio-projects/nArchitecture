using Core.Application.Dtos;

namespace Application.Features.FindeksCreditRates.Commands.UpdateByUserIdFromService;

public class UpdateByUserIdFindeksCreditRateFromServiceResponse : IDto
{
    public int Id { get; set; }
    public int Score { get; set; }
}