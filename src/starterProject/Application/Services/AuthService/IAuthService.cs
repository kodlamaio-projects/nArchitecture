using NArchitecture.Core.Security.Entities;
using NArchitecture.Core.Security.JWT;

namespace Application.Services.AuthService;

public interface IAuthService
{
    public Task<AccessToken> CreateAccessToken(User<int, int> user);
    public Task<RefreshToken<int, int>> CreateRefreshToken(User<int, int> user, string ipAddress);
    public Task<RefreshToken<int, int>?> GetRefreshTokenByToken(string token);
    public Task<RefreshToken<int, int>> AddRefreshToken(RefreshToken<int, int> refreshToken);
    public Task DeleteOldRefreshTokens(int userId);
    public Task RevokeDescendantRefreshTokens(RefreshToken<int, int> refreshToken, string ipAddress, string reason);

    public Task RevokeRefreshToken(RefreshToken<int, int> token, string ipAddress, string? reason = null, string? replacedByToken = null);

    public Task<RefreshToken<int, int>> RotateRefreshToken(User<int, int> user, RefreshToken<int, int> refreshToken, string ipAddress);
}
