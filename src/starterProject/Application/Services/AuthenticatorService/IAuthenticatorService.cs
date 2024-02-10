using Core.Security.Entities;

namespace Application.Services.AuthenticatorService;

public interface IAuthenticatorService
{
    public Task<EmailAuthenticator<int, int>> CreateEmailAuthenticator(User<int, int> user);
    public Task<OtpAuthenticator<int, int>> CreateOtpAuthenticator(User<int, int> user);
    public Task<string> ConvertSecretKeyToString(byte[] secretKey);
    public Task SendAuthenticatorCode(User<int, int> user);
    public Task VerifyAuthenticatorCode(User<int, int> user, string authenticatorCode);
}
