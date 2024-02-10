using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Mailing;
using Core.Security.EmailAuthenticator;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.OtpAuthenticator;
using MimeKit;

namespace Application.Services.AuthenticatorService;

public class AuthenticatorManager : IAuthenticatorService
{
    private readonly IEmailAuthenticatorHelper _emailAuthenticatorHelper;
    private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
    private readonly IMailService _mailService;
    private readonly IOtpAuthenticatorHelper _otpAuthenticatorHelper;
    private readonly IOtpAuthenticatorRepository _otpAuthenticatorRepository;

    public AuthenticatorManager(
        IEmailAuthenticatorHelper emailAuthenticatorHelper,
        IEmailAuthenticatorRepository emailAuthenticatorRepository,
        IMailService mailService,
        IOtpAuthenticatorHelper otpAuthenticatorHelper,
        IOtpAuthenticatorRepository otpAuthenticatorRepository
    )
    {
        _emailAuthenticatorHelper = emailAuthenticatorHelper;
        _emailAuthenticatorRepository = emailAuthenticatorRepository;
        _mailService = mailService;
        _otpAuthenticatorHelper = otpAuthenticatorHelper;
        _otpAuthenticatorRepository = otpAuthenticatorRepository;
    }

    public async Task<EmailAuthenticator<int, int>> CreateEmailAuthenticator(User<int, int> user)
    {
        EmailAuthenticator<int, int> emailAuthenticator =
            new()
            {
                UserId = user.Id,
                ActivationKey = await _emailAuthenticatorHelper.CreateEmailActivationKey(),
                IsVerified = false
            };
        return emailAuthenticator;
    }

    public async Task<OtpAuthenticator<int, int>> CreateOtpAuthenticator(User<int, int> user)
    {
        OtpAuthenticator<int, int> otpAuthenticator =
            new()
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

    public async Task SendAuthenticatorCode(User<int, int> user)
    {
        if (user.AuthenticatorType is AuthenticatorType.Email)
            await SendAuthenticatorCodeWithEmail(user);
    }

    public async Task VerifyAuthenticatorCode(User<int, int> user, string authenticatorCode)
    {
        if (user.AuthenticatorType is AuthenticatorType.Email)
            await VerifyAuthenticatorCodeWithEmail(user, authenticatorCode);
        else if (user.AuthenticatorType is AuthenticatorType.Otp)
            await VerifyAuthenticatorCodeWithOtp(user, authenticatorCode);
    }

    private async Task SendAuthenticatorCodeWithEmail(User<int, int> user)
    {
        EmailAuthenticator<int, int>? emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(predicate: e => e.UserId == user.Id);
        if (emailAuthenticator is null)
            throw new NotFoundException("Email Authenticator not found.");
        if (!emailAuthenticator.IsVerified)
            throw new BusinessException("Email Authenticator must be is verified.");

        string authenticatorCode = await _emailAuthenticatorHelper.CreateEmailActivationCode();
        emailAuthenticator.ActivationKey = authenticatorCode;
        await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);

        var toEmailList = new List<MailboxAddress> { new(name: user.Email, address: user.Email) };

        _mailService.SendMail(
            new Mail
            {
                ToList = toEmailList,
                Subject = "Authenticator Code - NArchitecture",
                TextBody = $"Enter your authenticator code: {authenticatorCode}"
            }
        );
    }

    private async Task VerifyAuthenticatorCodeWithEmail(User<int, int> user, string authenticatorCode)
    {
        EmailAuthenticator<int, int>? emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(predicate: e => e.UserId == user.Id);
        if (emailAuthenticator is null)
            throw new NotFoundException("Email Authenticator not found.");
        if (emailAuthenticator.ActivationKey != authenticatorCode)
            throw new BusinessException("Authenticator code is invalid.");
        emailAuthenticator.ActivationKey = null;
        await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);
    }

    private async Task VerifyAuthenticatorCodeWithOtp(User<int, int> user, string authenticatorCode)
    {
        OtpAuthenticator<int, int>? otpAuthenticator = await _otpAuthenticatorRepository.GetAsync(predicate: e => e.UserId == user.Id);
        if (otpAuthenticator is null)
            throw new NotFoundException("Otp Authenticator not found.");
        bool result = await _otpAuthenticatorHelper.VerifyCode(otpAuthenticator.SecretKey, authenticatorCode);
        if (!result)
            throw new BusinessException("Authenticator code is invalid.");
    }
}
