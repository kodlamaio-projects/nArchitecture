using FluentValidation;

namespace Application.Features.Colors.Commands.UpdateColor
{
    public class UpdateColorCommandValidator : AbstractValidator<UpdateColorCommand>
    {
        public UpdateColorCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MinimumLength(2);
        }
    }
}
