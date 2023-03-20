using Core.Application.Responses;

namespace Application.Features.FindeksCreditRates.Commands.Delete;

public class DeletedFindeksCreditRateResponse : IResponse
{
    public int Id { get; set; }
}
