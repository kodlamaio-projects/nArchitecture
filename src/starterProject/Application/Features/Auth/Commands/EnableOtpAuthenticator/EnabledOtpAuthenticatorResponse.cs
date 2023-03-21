using Core.Application.Responses;

namespace Application.Features.Auth.Commands.EnableOtpAuthenticator;

public class EnabledOtpAuthenticatorResponse : IResponse
{
    public string SecretKey { get; set; }
}
