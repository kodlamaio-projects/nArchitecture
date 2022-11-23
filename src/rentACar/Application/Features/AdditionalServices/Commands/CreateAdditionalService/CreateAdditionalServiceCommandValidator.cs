﻿using FluentValidation;

namespace Application.Features.AdditionalServices.Commands.CreateAdditionalService
{
    public class CreateAdditionalServiceCommandValidator : AbstractValidator<CreateAdditionalServiceCommand>
    {
        public CreateAdditionalServiceCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MinimumLength(2);
            RuleFor(c => c.DailyPrice).GreaterThan(0);
        }
    }
}
