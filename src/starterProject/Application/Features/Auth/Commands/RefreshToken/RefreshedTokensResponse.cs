using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Security.JWT;

namespace Application.Features.Auth.Commands.RefreshToken;

public class RefreshedTokensResponse : IResponse
{
    public AccessToken AccessToken { get; set; }
    public NArchitecture.Core.Security.Entities.RefreshToken<int, int> RefreshToken { get; set; }

    public RefreshedTokensResponse()
    {
        AccessToken = null!;
        RefreshToken = null!;
    }

    public RefreshedTokensResponse(AccessToken accessToken, NArchitecture.Core.Security.Entities.RefreshToken<int, int> refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}
