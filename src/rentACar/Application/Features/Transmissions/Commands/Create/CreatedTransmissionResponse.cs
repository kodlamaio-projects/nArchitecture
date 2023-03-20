using Core.Application.Responses;

namespace Application.Features.Transmissions.Commands.Create;

public class CreatedTransmissionResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}
