using Core.Application.Dtos;

namespace Application.Features.Fuels.Dtos;

public class UpdatedFuelDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}