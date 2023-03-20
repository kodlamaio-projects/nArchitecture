using Core.Application.Responses;

namespace Application.Features.AdditionalServices.Commands.Delete;

public class DeletedAdditionalServiceResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
}
