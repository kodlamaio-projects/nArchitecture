using Core.Application.Dtos;

namespace Application.Features.Transmissions.Dtos;

public class DeletedTransmissionDto : IDto
{
    public int Id { get; set; }
}