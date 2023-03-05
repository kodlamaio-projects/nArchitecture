using Core.Application.Dtos;

namespace Application.Features.FindeksCreditRates.Commands.Update;

public class UpdatedFindeksCreditRateResponse : IDto
{
    public int Id { get; set; }
    public int Score { get; set; }
}
