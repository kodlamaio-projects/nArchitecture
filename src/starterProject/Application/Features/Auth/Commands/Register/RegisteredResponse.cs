using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Security.JWT;

namespace Application.Features.Auth.Commands.Register;

public class RegisteredResponse : IResponse
{
    public AccessToken AccessToken { get; set; }
    public NArchitecture.Core.Security.Entities.RefreshToken<int, int> RefreshToken { get; set; }

    public RegisteredResponse()
    {
        AccessToken = null!;
        RefreshToken = null!;
    }

    public RegisteredResponse(AccessToken accessToken, NArchitecture.Core.Security.Entities.RefreshToken<int, int> refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}
