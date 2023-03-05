using FluentValidation;

namespace Application.Features.AdditionalServices.Commands.Update;

public class UpdateAdditionalServiceCommandValidator : AbstractValidator<UpdateAdditionalServiceCommand>
{
    public UpdateAdditionalServiceCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2);
        RuleFor(c => c.DailyPrice).GreaterThan(0);
    }
}
