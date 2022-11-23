﻿using FluentValidation;

namespace Application.Features.Fuels.Commands.CreateFuel
{
    public class CreateFuelCommandValidator : AbstractValidator<CreateFuelCommand>
    {
        public CreateFuelCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MinimumLength(2);
        }
    }
}
