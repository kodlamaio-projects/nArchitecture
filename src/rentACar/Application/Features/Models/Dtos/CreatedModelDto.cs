using Core.Application.Dtos;

namespace Application.Features.Models.Dtos;

public class CreatedModelDto : IDto
{
    public int Id { get; set; }
    public int BrandId { get; set; }
    public int FuelId { get; set; }
    public int TransmissionId { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public string ImageUrl { get; set; }
}