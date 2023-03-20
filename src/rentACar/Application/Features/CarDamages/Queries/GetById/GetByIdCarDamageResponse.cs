using Core.Application.Responses;

namespace Application.Features.CarDamages.Queries.GetById;

public class GetByIdCarDamageResponse : IResponse
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public string DamageDescription { get; set; }
    public bool IsFixed { get; set; }
}
