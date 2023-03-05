using Core.Application.Dtos;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserResponse : IDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Status { get; set; }
}
