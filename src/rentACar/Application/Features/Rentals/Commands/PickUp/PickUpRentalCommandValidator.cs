using FluentValidation;

namespace Application.Features.Rentals.Commands.PickUp
{
    public class PickUpRentalCommandValidator : AbstractValidator<PickUpRentalCommand>
    {
        public PickUpRentalCommandValidator()
        {
            RuleFor(c => c.RentEndRentalBranchId).GreaterThan(0);
            RuleFor(c => c.RentEndKilometer).GreaterThan(0);
            RuleFor(c => c.ReturnDate).GreaterThan(DateTime.Now);
        }
    }
}
