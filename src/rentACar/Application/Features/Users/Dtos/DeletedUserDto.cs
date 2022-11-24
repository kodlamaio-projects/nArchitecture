using Core.Application.Dtos;

namespace Application.Features.Users.Dtos;

public class DeletedUserDto : IDto
{
    public int Id { get; set; }
}