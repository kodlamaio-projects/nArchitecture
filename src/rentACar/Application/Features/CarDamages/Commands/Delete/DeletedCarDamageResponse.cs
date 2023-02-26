using Core.Application.Dtos;

namespace Application.Features.CarDamages.Commands.Delete;

public class DeletedCarDamageResponse : IDto
{
    public int Id { get; set; }
}