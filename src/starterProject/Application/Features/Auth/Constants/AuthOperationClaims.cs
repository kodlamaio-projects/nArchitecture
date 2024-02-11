using NArchitecture.Core.Security.Attributes;

namespace Application.Features.Auth.Constants;

[OperationClaimConstants]
public static class AuthOperationClaims
{
    private const string _section = "Auth";

    public const string Admin = $"{_section}.Admin";

    public const string Write = $"{_section}.Write";
    public const string Read = $"{_section}.Read";

    public const string RevokeToken = $"{_section}.RevokeToken";
}
