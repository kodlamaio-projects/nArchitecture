using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.VerifyEmailAuthenticator;

public class VerifyEmailAuthenticatorCommand : IRequest
{
    public string ActivationKey { get; set; }

    public class VerifyEmailAuthenticatorCommandHandler : IRequestHandler<VerifyEmailAuthenticatorCommand>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;

        public VerifyEmailAuthenticatorCommandHandler(
            IEmailAuthenticatorRepository emailAuthenticatorRepository,
            AuthBusinessRules authBusinessRules
        )
        {
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
            _authBusinessRules = authBusinessRules;
        }

        public async Task Handle(VerifyEmailAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            EmailAuthenticator? emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(
                e => e.ActivationKey == request.ActivationKey
            );
            await _authBusinessRules.EmailAuthenticatorShouldBeExists(emailAuthenticator);
            await _authBusinessRules.EmailAuthenticatorActivationKeyShouldBeExists(emailAuthenticator);

            emailAuthenticator.ActivationKey = null;
            emailAuthenticator.IsVerified = true;
            await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);
        }
    }
}
