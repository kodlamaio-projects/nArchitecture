using Application.Features.Auth.Commands.RevokeToken;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Features.Auth.Mappers;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class RefreshTokenMapper
{
    [MapperIgnoreTarget(nameof(RefreshToken.User))]
    public static partial RefreshToken ToRefreshToken(
        NArchitecture.Core.Security.Entities.RefreshToken<Guid, Guid> coreRefreshToken
    );

    public static partial RevokedTokenResponse ToRevokedResponse(RefreshToken refreshToken);
}
