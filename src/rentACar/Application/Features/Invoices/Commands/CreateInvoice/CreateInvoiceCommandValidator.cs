using FluentValidation;

namespace Application.Features.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator()
        {

        }
    }
}
