﻿using FluentValidation;

namespace Application.Features.CarDamages.Commands.CreateCarDamage
{
    public class CreateCarDamageCommandValidator : AbstractValidator<CreateCarDamageCommand>
    {
        public CreateCarDamageCommandValidator()
        {
            RuleFor(c => c.CarId).GreaterThan(0);
            RuleFor(c => c.DamageDescription).NotEmpty().MinimumLength(2);
        }
    }
}
