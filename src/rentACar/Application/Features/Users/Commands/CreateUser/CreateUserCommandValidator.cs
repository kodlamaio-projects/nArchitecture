using FluentValidation;

namespace Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().MinimumLength(2);
            RuleFor(c => c.LastName).NotEmpty().MinimumLength(2);
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.Password).NotEmpty().MinimumLength(4);
        }
    }
}
