using Core.Application.Dtos;

namespace Application.Features.Colors.Commands.Update;

public class UpdatedColorResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}
