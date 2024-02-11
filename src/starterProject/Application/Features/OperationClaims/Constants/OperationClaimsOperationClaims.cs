using NArchitecture.Core.Security.Attributes;

namespace Application.Features.OperationClaims.Constants;

[OperationClaimConstants]
public static class OperationClaimsOperationClaims
{
    private const string _section = "OperationClaims";

    public const string Admin = $"{_section}.Admin";

    public const string Read = $"{_section}.Read";
    public const string Write = $"{_section}.Write";

    public const string Add = $"{_section}.Add";
    public const string Update = $"{_section}.Update";
    public const string Delete = $"{_section}.Delete";
}
