using FluentValidation;

namespace Application.Features.Invoices.Commands.Update
{
    public class UpdateInvoiceCommandValidator : AbstractValidator<UpdateInvoiceCommand>
    {
        public UpdateInvoiceCommandValidator()
        {
            RuleFor(c => c.CustomerId).GreaterThan(0);
            RuleFor(c => c.RentalPrice).GreaterThan(0);
            RuleFor(c => c.RentalStartDate).LessThan(c => c.RentalEndDate);
            RuleFor(c => c.RentalEndDate).GreaterThan(c => c.RentalStartDate);
        }
    }
}
