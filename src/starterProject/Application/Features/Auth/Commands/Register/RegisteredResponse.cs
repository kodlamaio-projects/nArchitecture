using Core.Application.Responses;
using Core.Security.JWT;

namespace Application.Features.Auth.Commands.Register;

public class RegisteredResponse : IResponse
{
    public AccessToken AccessToken { get; set; }
    public Core.Security.Entities.RefreshToken<int, int> RefreshToken { get; set; }

    public RegisteredResponse()
    {
        AccessToken = null!;
        RefreshToken = null!;
    }

    public RegisteredResponse(AccessToken accessToken, Core.Security.Entities.RefreshToken<int, int> refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}
