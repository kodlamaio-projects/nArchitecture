using Core.Application.Dtos;

namespace Application.Features.Cars.Dtos;

public class DeletedCarDto : IDto
{
    public int Id { get; set; }
}