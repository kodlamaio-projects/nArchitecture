using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Features.Users.Commands.UpdateFromAuth;

public class UpdateUserFromAuthCommandValidator : AbstractValidator<UpdateUserFromAuthCommand>
{
    public UpdateUserFromAuthCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty().MinimumLength(2);
        RuleFor(c => c.LastName).NotEmpty().MinimumLength(2);
        RuleFor(c => c.Password).NotEmpty().MinimumLength(8);
        RuleFor(c => c.NewPassword)
            .NotEmpty()
            .MinimumLength(8)
            .Must(StrongPassword)
            .WithMessage(
                "Your password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, and one special character."
            );
    }

    private bool StrongPassword(string arg)
    {
        Regex regex = new("/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/");
        return regex.IsMatch(arg);
    }
}
