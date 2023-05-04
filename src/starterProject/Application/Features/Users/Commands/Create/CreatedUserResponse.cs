using Core.Application.Responses;

namespace Application.Features.Users.Commands.Create;

public class CreatedUserResponse : IResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Status { get; set; }

    public CreatedUserResponse()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
    }

    public CreatedUserResponse(int id, string firstName, string lastName, string email, bool status)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Status = status;
    }
}
