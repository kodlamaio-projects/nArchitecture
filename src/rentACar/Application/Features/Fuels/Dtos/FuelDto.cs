using Core.Application.Dtos;

namespace Application.Features.Fuels.Dtos;

public class FuelDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}