using FluentValidation;

namespace Application.Features.Auths.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.UserForLoginDto.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.UserForLoginDto.Password).NotEmpty().MinimumLength(4);
        }
    }
}
