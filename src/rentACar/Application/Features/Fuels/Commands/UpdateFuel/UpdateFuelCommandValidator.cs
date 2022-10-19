using FluentValidation;

namespace Application.Features.Fuels.Commands.UpdateFuel
{
    public class UpdateFuelCommandValidator : AbstractValidator<UpdateFuelCommand>
    {
        public UpdateFuelCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MinimumLength(2);
        }
    }
}
