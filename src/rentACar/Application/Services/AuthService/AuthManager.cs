using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Domain.Security.Entities;
using Core.Domain.Security.Enums;
using Core.Mailing;
using Core.Security.EmailAuthenticator;
using Core.Security.JWT;
using Core.Security.OtpAuthenticator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Services.AuthService;

public class AuthManager : IAuthService
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenHelper _tokenHelper;
    private readonly TokenOptions _tokenOptions;
    private readonly IMailService _mailService;
    private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
    private readonly IEmailAuthenticatorHelper _emailAuthenticatorHelper;
    private readonly IOtpAuthenticatorRepository _otpAuthenticatorRepository;
    private readonly IOtpAuthenticatorHelper _otpAuthenticatorHelper;

    public AuthManager(IUserOperationClaimRepository userOperationClaimRepository,
                       IRefreshTokenRepository refreshTokenRepository, ITokenHelper tokenHelper,
                       IConfiguration configuration, IEmailAuthenticatorHelper emailAuthenticatorHelper,
                       IMailService mailService, IEmailAuthenticatorRepository emailAuthenticatorRepository,
                       IOtpAuthenticatorHelper otpAuthenticatorHelper,
                       IOtpAuthenticatorRepository otpAuthenticatorRepository)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenHelper = tokenHelper;
        _emailAuthenticatorHelper = emailAuthenticatorHelper;
        _mailService = mailService;
        _emailAuthenticatorRepository = emailAuthenticatorRepository;
        _otpAuthenticatorHelper = otpAuthenticatorHelper;
        _otpAuthenticatorRepository = otpAuthenticatorRepository;
        _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
    }

    public async Task<AccessToken> CreateAccessToken(User user)
    {
        IList<OperationClaim> operationClaims = await _userOperationClaimRepository
                .Query()
                .AsNoTracking()
                .Where(p => p.UserId == user.Id)
                .Select(p => new OperationClaim
                {
                    Id = p.OperationClaimId,
                    Name = p.OperationClaim.Name
                })
                .ToListAsync();

        AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);
        return accessToken;
    }

    public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
    {
        RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
        return addedRefreshToken;
    }

    public async Task DeleteOldRefreshTokens(int userId)
    {
        IList<RefreshToken> refreshTokens = (await _refreshTokenRepository.GetListAsync(r =>
                                                 r.UserId == userId &&
                                                 r.Revoked == null && r.Expires >= DateTime.UtcNow &&
                                                 r.Created.AddDays(_tokenOptions.RefreshTokenTTL) <=
                                                 DateTime.UtcNow)
                                            ).Items;
        foreach (RefreshToken refreshToken in refreshTokens) await _refreshTokenRepository.DeleteAsync(refreshToken);
    }

    public async Task<RefreshToken?> GetRefreshTokenByToken(string token)
    {
        RefreshToken? refreshToken = await _refreshTokenRepository.GetAsync(r => r.Token == token);
        return refreshToken;
    }

    public async Task RevokeRefreshToken(RefreshToken refreshToken, string ipAddress, string? reason = null,
                                         string? replacedByToken = null)
    {
        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;
        refreshToken.ReasonRevoked = reason;
        refreshToken.ReplacedByToken = replacedByToken;
        await _refreshTokenRepository.UpdateAsync(refreshToken);
    }

    public async Task<RefreshToken> RotateRefreshToken(User user, RefreshToken refreshToken, string ipAddress)
    {
        RefreshToken newRefreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);
        await RevokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
        return newRefreshToken;
    }

    public async Task RevokeDescendantRefreshTokens(RefreshToken refreshToken, string ipAddress,
                                                    string reason)
    {
        RefreshToken childToken = await _refreshTokenRepository.GetAsync(r => r.Token == refreshToken.ReplacedByToken);

        if (childToken != null && childToken.Revoked != null && childToken.Expires <= DateTime.UtcNow)
            await RevokeRefreshToken(childToken, ipAddress, reason);
        else await RevokeDescendantRefreshTokens(childToken, ipAddress, reason);
    }

    public Task<RefreshToken> CreateRefreshToken(User user, string ipAddress)
    {
        RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);
        return Task.FromResult(refreshToken);
    }

    public async Task<EmailAuthenticator> CreateEmailAuthenticator(User user)
    {
        EmailAuthenticator emailAuthenticator = new()
        {
            UserId = user.Id,
            ActivationKey = await _emailAuthenticatorHelper.CreateEmailActivationKey(),
            IsVerified = false
        };
        return emailAuthenticator;
    }

    public async Task<OtpAuthenticator> CreateOtpAuthenticator(User user)
    {
        OtpAuthenticator otpAuthenticator = new()
        {
            UserId = user.Id,
            SecretKey = await _otpAuthenticatorHelper.GenerateSecretKey(),
            IsVerified = false
        };
        return otpAuthenticator;
    }

    public async Task<string> ConvertSecretKeyToString(byte[] secretKey)
    {
        string result = await _otpAuthenticatorHelper.ConvertSecretKeyToString(secretKey);
        return result;
    }

    public async Task SendAuthenticatorCode(User user)
    {
        if (user.AuthenticatorType is AuthenticatorType.Email) await sendAuthenticatorCodeWithEmail(user);
    }

    public async Task VerifyAuthenticatorCode(User user, string authenticatorCode)
    {
        if (user.AuthenticatorType is AuthenticatorType.Email)
            await verifyAuthenticatorCodeWithEmail(user, authenticatorCode);
        else if (user.AuthenticatorType is AuthenticatorType.Otp)
            await verifyAuthenticatorCodeWithOtp(user, authenticatorCode);
    }

    private async Task sendAuthenticatorCodeWithEmail(User user)
    {
        EmailAuthenticator emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(e => e.UserId == user.Id);

        if (!emailAuthenticator.IsVerified) throw new BusinessException("Email Authenticator must be is verified.");

        string authenticatorCode = await _emailAuthenticatorHelper.CreateEmailActivationCode();
        emailAuthenticator.ActivationKey = authenticatorCode;
        await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);

        _mailService.SendMail(new Mail
        {
            ToEmail = user.Email,
            ToFullName = $"{user.FirstName} {user.LastName}",
            Subject = "Authenticator Code - RentACar",
            TextBody = $"Enter your authenticator code: {authenticatorCode}"
        });
    }

    private async Task verifyAuthenticatorCodeWithEmail(User user, string authenticatorCode)
    {
        EmailAuthenticator emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(e => e.UserId == user.Id);

        if (emailAuthenticator.ActivationKey != authenticatorCode)
            throw new BusinessException("Authenticator code is invalid.");

        emailAuthenticator.ActivationKey = null;
        await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);
    }

    private async Task verifyAuthenticatorCodeWithOtp(User user, string authenticatorCode)
    {
        OtpAuthenticator otpAuthenticator = await _otpAuthenticatorRepository.GetAsync(e => e.UserId == user.Id);

        bool result = await _otpAuthenticatorHelper.VerifyCode(otpAuthenticator.SecretKey, authenticatorCode);

        if (!result)
            throw new BusinessException("Authenticator code is invalid.");
    }
}