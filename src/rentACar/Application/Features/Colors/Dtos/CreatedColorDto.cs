using Core.Application.Dtos;

namespace Application.Features.Colors.Dtos;

public class CreatedColorDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}