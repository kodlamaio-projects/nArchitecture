using FluentValidation;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(c => c.UserForRegisterDto.FirstName).NotEmpty().MinimumLength(2);
            RuleFor(c => c.UserForRegisterDto.LastName).NotEmpty().MinimumLength(2);
            RuleFor(c => c.UserForRegisterDto.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.UserForRegisterDto.Password).NotEmpty().MinimumLength(4);
        }
    }
}
