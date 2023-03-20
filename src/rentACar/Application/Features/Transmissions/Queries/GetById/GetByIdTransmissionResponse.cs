using Core.Application.Responses;

namespace Application.Features.Transmissions.Queries.GetById;

public class GetByIdTransmissionResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}
