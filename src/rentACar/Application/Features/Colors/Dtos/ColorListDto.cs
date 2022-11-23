using Core.Application.Dtos;

namespace Application.Features.Colors.Dtos;

public class ColorListDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}