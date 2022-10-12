using FluentValidation;

namespace Application.Features.Transmissions.Commands.CreateTransmission
{
    public class CreateTransmissionCommandValidator : AbstractValidator<CreateTransmissionCommand>
    {
        public CreateTransmissionCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MinimumLength(2);
        }
    }
}
