using Application.Features.Cars.Validations;
using FluentValidation;

namespace Application.Features.Cars.Commands.Update;

public class UpdateCarCommandValidator : AbstractValidator<UpdateCarCommand>
{
    public UpdateCarCommandValidator()
    {
        RuleFor(c => c.ModelYear).GreaterThan((short)1900);
        RuleFor(c => c.Plate).NotEmpty().Must(CarCustomValidationRules.IsTurkeyPlate).WithMessage("Plate is not valid.");
    }
}
