using FluentValidation;

namespace Application.Features.Rentals.Commands.Update;

public class UpdateRentalCommandValidator : AbstractValidator<UpdateRentalCommand>
{
    public UpdateRentalCommandValidator()
    {
        RuleFor(c => c.RentStartDate)
            .LessThan(c => c.RentEndDate);
        RuleFor(c => c.RentEndDate)
            .GreaterThan(c => c.RentStartDate);
    }
}