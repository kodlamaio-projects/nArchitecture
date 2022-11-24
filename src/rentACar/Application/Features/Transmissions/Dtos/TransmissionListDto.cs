using Core.Application.Dtos;

namespace Application.Features.Transmissions.Dtos;

public class TransmissionListDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}