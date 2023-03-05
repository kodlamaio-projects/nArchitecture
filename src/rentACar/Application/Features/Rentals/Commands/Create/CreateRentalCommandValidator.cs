using FluentValidation;

namespace Application.Features.Rentals.Commands.Create;

public class CreateRentalCommandValidator : AbstractValidator<CreateRentalCommand>
{
    public CreateRentalCommandValidator()
    {
        RuleFor(c => c.RentStartDate).GreaterThan(DateTime.Now).LessThan(c => c.RentEndDate);
        RuleFor(c => c.RentEndDate).GreaterThan(c => c.RentStartDate);
    }
}
