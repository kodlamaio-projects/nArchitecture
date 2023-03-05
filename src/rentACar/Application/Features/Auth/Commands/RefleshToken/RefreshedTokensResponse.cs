using Core.Application.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Features.Auth.Commands.RefleshToken;

public class RefreshedTokensResponse : IDto
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
}
