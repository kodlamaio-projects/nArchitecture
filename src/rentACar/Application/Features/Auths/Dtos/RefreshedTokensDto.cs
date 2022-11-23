using Core.Application.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Features.Auths.Dtos;

public class RefreshedTokensDto : IDto
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
}