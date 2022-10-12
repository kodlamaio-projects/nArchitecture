using FluentValidation;

namespace Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().MinimumLength(2);
            RuleFor(c => c.LastName).NotEmpty().MinimumLength(2);
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.Password).NotEmpty().MinimumLength(4);
        }
    }
}
