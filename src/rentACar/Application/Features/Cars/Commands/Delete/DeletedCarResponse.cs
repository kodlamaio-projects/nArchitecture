using Core.Application.Dtos;

namespace Application.Features.Cars.Commands.Delete;

public class DeletedCarResponse : IDto
{
    public int Id { get; set; }
}