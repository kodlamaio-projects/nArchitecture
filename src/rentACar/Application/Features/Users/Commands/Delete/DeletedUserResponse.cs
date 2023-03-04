using Core.Application.Dtos;

namespace Application.Features.Users.Commands.Delete;

public class DeletedUserResponse : IDto
{
    public int Id { get; set; }
}