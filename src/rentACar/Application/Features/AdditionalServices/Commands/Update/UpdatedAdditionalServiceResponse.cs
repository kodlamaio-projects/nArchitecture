using Core.Application.Responses;

namespace Application.Features.AdditionalServices.Commands.Update;

public class UpdatedAdditionalServiceResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
}
