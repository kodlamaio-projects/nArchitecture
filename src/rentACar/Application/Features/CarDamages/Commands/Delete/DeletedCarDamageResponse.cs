using Core.Application.Responses;

namespace Application.Features.CarDamages.Commands.Delete;

public class DeletedCarDamageResponse : IResponse
{
    public int Id { get; set; }
}
