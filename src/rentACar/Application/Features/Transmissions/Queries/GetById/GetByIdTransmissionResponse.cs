using Core.Application.Dtos;

namespace Application.Features.Transmissions.Queries.GetById;

public class GetByIdTransmissionResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}