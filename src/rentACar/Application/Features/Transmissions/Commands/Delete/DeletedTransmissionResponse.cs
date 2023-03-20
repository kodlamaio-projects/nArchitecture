using Core.Application.Responses;

namespace Application.Features.Transmissions.Commands.Delete;

public class DeletedTransmissionResponse : IResponse
{
    public int Id { get; set; }
}
