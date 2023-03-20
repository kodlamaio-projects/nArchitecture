using Core.Application.Responses;

namespace Application.Features.Models.Commands.Update;

public class UpdatedModelResponse : IResponse
{
    public int Id { get; set; }
    public int BrandId { get; set; }
    public int FuelId { get; set; }
    public int TransmissionId { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public string ImageUrl { get; set; }
}
