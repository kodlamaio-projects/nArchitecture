using Core.Application.Responses;

namespace Application.Features.Transmissions.Commands.Update;

public class UpdatedTransmissionResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}
