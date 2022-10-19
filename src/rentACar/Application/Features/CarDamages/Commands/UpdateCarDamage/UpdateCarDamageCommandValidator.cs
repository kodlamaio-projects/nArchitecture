using FluentValidation;

namespace Application.Features.CarDamages.Commands.UpdateCarDamage
{
    public class UpdateCarDamageCommandValidator : AbstractValidator<UpdateCarDamageCommand>
    {
        public UpdateCarDamageCommandValidator()
        {
            RuleFor(c => c.CarId).GreaterThan(0);
            RuleFor(c => c.DamageDescription).NotEmpty().MinimumLength(2);
        }
    }
}
