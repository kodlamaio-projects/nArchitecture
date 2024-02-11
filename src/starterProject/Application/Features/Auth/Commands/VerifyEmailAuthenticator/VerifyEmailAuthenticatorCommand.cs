using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using MediatR;
using NArchitecture.Core.Security.Entities;

namespace Application.Features.Auth.Commands.VerifyEmailAuthenticator;

public class VerifyEmailAuthenticatorCommand : IRequest
{
    public string ActivationKey { get; set; }

    public VerifyEmailAuthenticatorCommand()
    {
        ActivationKey = string.Empty;
    }

    public VerifyEmailAuthenticatorCommand(string activationKey)
    {
        ActivationKey = activationKey;
    }

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
            EmailAuthenticator<int, int>? emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(
                predicate: e => e.ActivationKey == request.ActivationKey,
                cancellationToken: cancellationToken
            );
            await _authBusinessRules.EmailAuthenticatorShouldBeExists(emailAuthenticator);
            await _authBusinessRules.EmailAuthenticatorActivationKeyShouldBeExists(emailAuthenticator!);

            emailAuthenticator!.ActivationKey = null;
            emailAuthenticator.IsVerified = true;
            await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);
        }
    }
}
