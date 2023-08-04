using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Application.Services.TranslateService;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;

namespace Application.Features.Auth.Rules;

public class AuthBusinessRules : BaseBusinessRules
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
    private readonly ITranslateService _translateService;

    public AuthBusinessRules(
        IUserRepository userRepository,
        IEmailAuthenticatorRepository emailAuthenticatorRepository,
        ITranslateService translateService
        )
    {
        _userRepository = userRepository;
        _emailAuthenticatorRepository = emailAuthenticatorRepository;
        _translateService = translateService;
    }

    public override async Task ThrowBusinessException(string message)
    {
        string translatedMessage = await _translateService.TranslateAsync(message);
        await base.ThrowBusinessException(translatedMessage);
    }

    public async Task EmailAuthenticatorShouldBeExists(EmailAuthenticator? emailAuthenticator)
    {
        if (emailAuthenticator is null)
            await ThrowBusinessException(AuthMessages.EmailAuthenticatorDontExists);
    }

    public async Task OtpAuthenticatorShouldBeExists(OtpAuthenticator? otpAuthenticator)
    {
        if (otpAuthenticator is null)
            await ThrowBusinessException(AuthMessages.OtpAuthenticatorDontExists);
    }

    public async Task OtpAuthenticatorThatVerifiedShouldNotBeExists(OtpAuthenticator? otpAuthenticator)
    {
        if (otpAuthenticator is not null && otpAuthenticator.IsVerified)
            await ThrowBusinessException(AuthMessages.AlreadyVerifiedOtpAuthenticatorIsExists);
    }

    public async Task EmailAuthenticatorActivationKeyShouldBeExists(EmailAuthenticator emailAuthenticator)
    {
        if (emailAuthenticator.ActivationKey is null)
            await ThrowBusinessException(AuthMessages.EmailActivationKeyDontExists);
    }

    public async Task UserShouldBeExistsWhenSelected(User? user)
    {
        if (user == null)
            await ThrowBusinessException(AuthMessages.UserDontExists);
    }

    public async Task UserShouldNotBeHaveAuthenticator(User user)
    {
        if (user.AuthenticatorType != AuthenticatorType.None)
            await ThrowBusinessException(AuthMessages.UserHaveAlreadyAAuthenticator);
    }

    public async Task RefreshTokenShouldBeExists(RefreshToken? refreshToken)
    {
        if (refreshToken == null)
            await ThrowBusinessException(AuthMessages.RefreshDontExists);
    }

    public async Task RefreshTokenShouldBeActive(RefreshToken refreshToken)
    {
        if (refreshToken.Revoked != null && DateTime.UtcNow >= refreshToken.Expires)
            await ThrowBusinessException(AuthMessages.InvalidRefreshToken);
    }

    public async Task UserEmailShouldBeNotExists(string email)
    {
        bool doesExists = await _userRepository.AnyAsync(predicate: u => u.Email == email, enableTracking: false);
        if (doesExists)
            await ThrowBusinessException(AuthMessages.UserMailAlreadyExists);
    }

    public async Task UserPasswordShouldBeMatch(int id, string password)
    {
        User? user = await _userRepository.GetAsync(predicate: u => u.Id == id, enableTracking: false);
        await UserShouldBeExistsWhenSelected(user);
        if (!HashingHelper.VerifyPasswordHash(password, user!.PasswordHash, user.PasswordSalt))
            await ThrowBusinessException(AuthMessages.PasswordDontMatch);
    }
}
