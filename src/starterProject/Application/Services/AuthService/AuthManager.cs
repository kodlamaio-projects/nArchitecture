using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using Core.Security.Entities;
using Core.Security.JWT;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Services.AuthService;

public class AuthManager : IAuthService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenHelper<int, int> _tokenHelper;
    private readonly TokenOptions _tokenOptions;
    private readonly AuthBusinessRules _authBusinessRules;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

    public AuthManager(
        IUserOperationClaimRepository userOperationClaimRepository,
        IRefreshTokenRepository refreshTokenRepository,
        ITokenHelper<int, int> tokenHelper,
        IConfiguration configuration,
        AuthBusinessRules authBusinessRules
    )
    {
        _userOperationClaimRepository = userOperationClaimRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenHelper = tokenHelper;
        _authBusinessRules = authBusinessRules;

        const string tokenOptionsConfigurationSection = "TokenOptions";
        _tokenOptions =
            configuration.GetSection(tokenOptionsConfigurationSection).Get<TokenOptions>()
            ?? throw new NullReferenceException($"\"{tokenOptionsConfigurationSection}\" section cannot found in configuration");
    }

    public async Task<AccessToken> CreateAccessToken(User<int, int> user)
    {
        IList<OperationClaim<int, int>> operationClaims = await _userOperationClaimRepository.GetOperationClaimsByUserIdAsync(user.Id);
        AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);
        return accessToken;
    }

    public async Task<RefreshToken<int, int>> AddRefreshToken(RefreshToken<int, int> refreshToken)
    {
        RefreshToken<int, int> addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
        return addedRefreshToken;
    }

    public async Task DeleteOldRefreshTokens(int userId)
    {
        List<RefreshToken<int, int>> refreshTokens = await _refreshTokenRepository.GetOldRefreshTokensAsync(userId, _tokenOptions.RefreshTokenTTL);
        await _refreshTokenRepository.DeleteRangeAsync(refreshTokens);
    }

    public async Task<RefreshToken<int, int>?> GetRefreshTokenByToken(string token)
    {
        RefreshToken<int, int>? refreshToken = await _refreshTokenRepository.GetAsync(predicate: r => r.Token == token);
        return refreshToken;
    }

    public async Task RevokeRefreshToken(RefreshToken<int, int> refreshToken, string ipAddress, string? reason = null, string? replacedByToken = null)
    {
        refreshToken.RevokedDate = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;
        refreshToken.ReasonRevoked = reason;
        refreshToken.ReplacedByToken = replacedByToken;
        await _refreshTokenRepository.UpdateAsync(refreshToken);
    }

    public async Task<RefreshToken<int, int>> RotateRefreshToken(User<int, int> user, RefreshToken<int, int> refreshToken, string ipAddress)
    {
        RefreshToken<int, int> newRefreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);
        await RevokeRefreshToken(refreshToken, ipAddress, reason: "Replaced by new token", newRefreshToken.Token);
        return newRefreshToken;
    }

    public async Task RevokeDescendantRefreshTokens(RefreshToken<int, int> refreshToken, string ipAddress, string reason)
    {
        RefreshToken<int, int>? childToken = await _refreshTokenRepository.GetAsync(predicate: r => r.Token == refreshToken.ReplacedByToken);

        if (childToken?.RevokedDate != null && childToken.ExpiresDate <= DateTime.UtcNow)
            await RevokeRefreshToken(childToken, ipAddress, reason);
        else
            await RevokeDescendantRefreshTokens(refreshToken: childToken!, ipAddress, reason);
    }

    public Task<RefreshToken<int, int>> CreateRefreshToken(User<int, int> user, string ipAddress)
    {
        RefreshToken<int, int> refreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);
        return Task.FromResult(refreshToken);
    }
}
