using Core.Application.Responses;

namespace Application.Features.CarDamages.Commands.Create;

public class CreatedCarDamageResponse : IResponse
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public string DamageDescription { get; set; }
    public bool IsFixed { get; set; }
}
