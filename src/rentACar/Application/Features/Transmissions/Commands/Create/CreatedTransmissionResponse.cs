using Core.Application.Dtos;

namespace Application.Features.Transmissions.Commands.Create;

public class CreatedTransmissionResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}
