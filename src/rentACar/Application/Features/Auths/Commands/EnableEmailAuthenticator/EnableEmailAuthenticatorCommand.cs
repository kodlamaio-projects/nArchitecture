using System.Web;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Application.Services.UserService;
using Core.Domain.Concrete.Security.Entities;
using Core.Domain.Concrete.Security.Enums;
using Core.Mailing;
using MediatR;
using MimeKit;

namespace Application.Features.Auths.Commands.EnableEmailAuthenticator;

public class EnableEmailAuthenticatorCommand : IRequest
{
    public int UserId { get; set; }
    public string VerifyEmailUrlPrefix { get; set; }

    public class EnableEmailAuthenticatorCommandHandler : IRequestHandler<EnableEmailAuthenticatorCommand>
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
        private readonly IMailService _mailService;
        private readonly AuthBusinessRules _authBusinessRules;

        public EnableEmailAuthenticatorCommandHandler(IUserService userService, IAuthService authService,
                                                      IEmailAuthenticatorRepository emailAuthenticatorRepository,
                                                      IMailService mailService, AuthBusinessRules authBusinessRules)
        {
            _userService = userService;
            _authService = authService;
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
            _mailService = mailService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<Unit> Handle(EnableEmailAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetById(request.UserId);
            await _authBusinessRules.UserShouldBeExists(user);
            await _authBusinessRules.UserShouldNotBeHaveAuthenticator(user);

            user.AuthenticatorType = AuthenticatorType.Email;
            await _userService.Update(user);

            EmailAuthenticator emailAuthenticator = await _authService.CreateEmailAuthenticator(user);
            EmailAuthenticator addedEmailAuthenticator =
                await _emailAuthenticatorRepository.AddAsync(emailAuthenticator);

            var toEmailList = new List<MailboxAddress>
            {
                new($"{user.FirstName} {user.LastName}",user.Email)
            };

            _mailService.SendMail(new Mail
            {
                ToList = toEmailList,
                Subject = "Verify Your Email - RentACar",
                TextBody =
                $"Click on the link to verify your email: {request.VerifyEmailUrlPrefix}?ActivationKey={HttpUtility.UrlEncode(addedEmailAuthenticator.ActivationKey)}"
            });

            return Unit.Value; //todo: return dto?
        }
    }
}