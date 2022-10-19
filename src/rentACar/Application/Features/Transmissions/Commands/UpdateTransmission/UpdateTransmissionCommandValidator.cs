using FluentValidation;

namespace Application.Features.Transmissions.Commands.UpdateTransmission
{
    public class UpdateTransmissionCommandValidator : AbstractValidator<UpdateTransmissionCommand>
    {
        public UpdateTransmissionCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MinimumLength(2);
        }
    }
}
