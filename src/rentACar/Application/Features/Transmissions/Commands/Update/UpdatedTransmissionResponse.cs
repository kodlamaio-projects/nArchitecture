using Core.Application.Dtos;

namespace Application.Features.Transmissions.Commands.Update;

public class UpdatedTransmissionResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}
