using Core.Application.Responses;

namespace Application.Features.CarDamages.Commands.Update;

public class UpdatedCarDamageResponse : IResponse
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public string DamageDescription { get; set; }
    public bool IsFixed { get; set; }
}
