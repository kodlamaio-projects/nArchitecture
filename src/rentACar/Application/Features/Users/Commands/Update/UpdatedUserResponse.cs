using Core.Application.Dtos;

namespace Application.Features.Users.Commands.Update;

public class UpdatedUserResponse : IDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Status { get; set; }
}