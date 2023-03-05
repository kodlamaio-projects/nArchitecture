using FluentValidation;

namespace Application.Features.FindeksCreditRates.Commands.UpdateFromService;

public class UpdateFindeksCreditRateFromServiceCommandValidator : AbstractValidator<UpdateFindeksCreditRateFromServiceCommand>
{
    public UpdateFindeksCreditRateFromServiceCommandValidator()
    {
        RuleFor(c => c.IdentityNumber).NotEmpty().MinimumLength(2);
    }
}
