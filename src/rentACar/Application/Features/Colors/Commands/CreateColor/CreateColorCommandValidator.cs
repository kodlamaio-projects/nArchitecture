using FluentValidation;

namespace Application.Features.Colors.Commands.CreateColor
{
    public class CreateColorCommandValidator : AbstractValidator<CreateColorCommand>
    {
        public CreateColorCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MinimumLength(2);
        }
    }
}
