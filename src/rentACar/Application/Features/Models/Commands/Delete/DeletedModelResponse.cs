using Core.Application.Dtos;

namespace Application.Features.Models.Commands.Delete;

public class DeletedModelResponse : IDto
{
    public int Id { get; set; }
}
