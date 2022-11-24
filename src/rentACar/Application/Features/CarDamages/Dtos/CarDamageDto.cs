using Core.Application.Dtos;

namespace Application.Features.CarDamages.Dtos;

public class CarDamageDto : IDto
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public string DamageDescription { get; set; }
    public bool IsFixed { get; set; }
}