using Core.Application.Dtos;

namespace Application.Features.Auths.Dtos;

public class RevokedTokenDto : IDto
{
    public int Id { get; set; }
    public string Token { get; set; }
}