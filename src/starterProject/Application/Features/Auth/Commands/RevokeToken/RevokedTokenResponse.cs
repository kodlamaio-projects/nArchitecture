using NArchitecture.Core.Application.Responses;

namespace Application.Features.Auth.Commands.RevokeToken;

public class RevokedTokenResponse : IResponse
{
    public Guid Id { get; set; }
    public string Token { get; set; }

    public RevokedTokenResponse()
    {
        Token = string.Empty;
    }

    public RevokedTokenResponse(Guid id, string token)
    {
        Id = id;
        Token = token;
    }
}
