using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Security.JWT;

namespace Application.Features.Users.Commands.UpdateFromAuth;

public class UpdatedUserFromAuthResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public AccessToken AccessToken { get; set; }

    public UpdatedUserFromAuthResponse()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        AccessToken = null!;
    }

    public UpdatedUserFromAuthResponse(Guid id, string firstName, string lastName, string email, AccessToken accessToken)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        AccessToken = accessToken;
    }
}
