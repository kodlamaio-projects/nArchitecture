using Core.Application.Dtos;

namespace Application.Features.Fuels.Commands.Delete;

public class DeletedFuelResponse : IDto
{
    public int Id { get; set; }
}