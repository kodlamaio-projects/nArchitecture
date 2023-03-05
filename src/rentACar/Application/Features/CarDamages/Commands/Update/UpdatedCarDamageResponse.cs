using Core.Application.Dtos;

namespace Application.Features.CarDamages.Commands.Update;

public class UpdatedCarDamageResponse : IDto
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public string DamageDescription { get; set; }
    public bool IsFixed { get; set; }
}
