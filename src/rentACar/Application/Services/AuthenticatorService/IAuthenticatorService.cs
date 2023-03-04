using Core.Security.Entities;

namespace Application.Services.AuthenticatorService;

public interface IAuthenticatorService
{
    public Task<EmailAuthenticator> CreateEmailAuthenticator(User user);
    public Task<OtpAuthenticator> CreateOtpAuthenticator(User user);
    public Task<string> ConvertSecretKeyToString(byte[] secretKey);
    public Task SendAuthenticatorCode(User user);
    public Task VerifyAuthenticatorCode(User user, string authenticatorCode);
}