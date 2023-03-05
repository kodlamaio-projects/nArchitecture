using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.RevokeToken;

public class RevokeTokenCommand : IRequest<RevokedTokenResponse>
{
    public string Token { get; set; }
    public string IPAddress { get; set; }

    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, RevokedTokenResponse>
    {
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IMapper _mapper;

        public RevokeTokenCommandHandler(IAuthService authService, AuthBusinessRules authBusinessRules, IMapper mapper)
        {
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _mapper = mapper;
        }

        public async Task<RevokedTokenResponse> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            RefreshToken? refreshToken = await _authService.GetRefreshTokenByToken(request.Token);
            await _authBusinessRules.RefreshTokenShouldBeExists(refreshToken);
            await _authBusinessRules.RefreshTokenShouldBeActive(refreshToken);

            await _authService.RevokeRefreshToken(refreshToken, request.IPAddress, reason: "Revoked without replacement");

            RevokedTokenResponse revokedTokenResponse = _mapper.Map<RevokedTokenResponse>(refreshToken);
            return revokedTokenResponse;
        }
    }
}
