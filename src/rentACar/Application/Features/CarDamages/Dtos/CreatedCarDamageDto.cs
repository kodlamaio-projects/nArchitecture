using Core.Application.Dtos;

namespace Application.Features.CarDamages.Dtos;

public class CreatedCarDamageDto : IDto
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public string DamageDescription { get; set; }
    public bool IsFixed { get; set; }
}