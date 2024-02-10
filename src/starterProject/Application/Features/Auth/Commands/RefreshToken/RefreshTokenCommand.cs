using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using Application.Services.UsersService;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<RefreshedTokensResponse>
{
    public string RefreshToken { get; set; }
    public string IpAddress { get; set; }

    public RefreshTokenCommand()
    {
        RefreshToken = string.Empty;
        IpAddress = string.Empty;
    }

    public RefreshTokenCommand(string refreshToken, string ipAddress)
    {
        RefreshToken = refreshToken;
        IpAddress = ipAddress;
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshedTokensResponse>
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly AuthBusinessRules _authBusinessRules;

        public RefreshTokenCommandHandler(IAuthService authService, IUserService userService, AuthBusinessRules authBusinessRules)
        {
            _authService = authService;
            _userService = userService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<RefreshedTokensResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            Core.Security.Entities.RefreshToken<int, int>? refreshToken = await _authService.GetRefreshTokenByToken(request.RefreshToken);
            await _authBusinessRules.RefreshTokenShouldBeExists(refreshToken);

            if (refreshToken!.RevokedDate != null)
                await _authService.RevokeDescendantRefreshTokens(
                    refreshToken,
                    request.IpAddress,
                    reason: $"Attempted reuse of revoked ancestor token: {refreshToken.Token}"
                );
            await _authBusinessRules.RefreshTokenShouldBeActive(refreshToken);

            User<int, int>? user = await _userService.GetAsync(
                predicate: u => u.Id == refreshToken.UserId,
                cancellationToken: cancellationToken
            );
            await _authBusinessRules.UserShouldBeExistsWhenSelected(user);

            Core.Security.Entities.RefreshToken<int, int> newRefreshToken = await _authService.RotateRefreshToken(
                user: user!,
                refreshToken,
                request.IpAddress
            );
            Core.Security.Entities.RefreshToken<int, int> addedRefreshToken = await _authService.AddRefreshToken(newRefreshToken);
            await _authService.DeleteOldRefreshTokens(refreshToken.UserId);

            AccessToken createdAccessToken = await _authService.CreateAccessToken(user!);

            RefreshedTokensResponse refreshedTokensResponse = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
            return refreshedTokensResponse;
        }
    }
}
