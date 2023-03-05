using FluentValidation;

namespace Application.Features.FindeksCreditRates.Commands.Update;

public class UpdateFindeksCreditRateCommandValidator : AbstractValidator<UpdateFindeksCreditRateCommand>
{
    public UpdateFindeksCreditRateCommandValidator()
    {
        RuleFor(f => f.Score).GreaterThanOrEqualTo(Convert.ToInt16(0)).LessThanOrEqualTo(Convert.ToInt16(1900));
    }
}
