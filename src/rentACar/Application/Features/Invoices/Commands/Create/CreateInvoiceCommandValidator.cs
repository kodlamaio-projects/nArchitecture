using FluentValidation;

namespace Application.Features.Invoices.Commands.Create
{
    public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator()
        {
            RuleFor(c => c.CustomerId).GreaterThan(0);
            RuleFor(c => c.RentalPrice).GreaterThan(0);
            RuleFor(c => c.RentalStartDate).LessThan(c => c.RentalEndDate);
            RuleFor(c => c.RentalEndDate).GreaterThan(c => c.RentalStartDate);
        }
    }
}
