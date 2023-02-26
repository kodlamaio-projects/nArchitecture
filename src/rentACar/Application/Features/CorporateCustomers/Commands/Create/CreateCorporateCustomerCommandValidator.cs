using FluentValidation;

namespace Application.Features.CorporateCustomers.Commands.Create
{
    public class CreateCorporateCustomerCommandValidator : AbstractValidator<CreateCorporateCustomerCommand>
    {
        public CreateCorporateCustomerCommandValidator()
        {
            RuleFor(c => c.CustomerId).GreaterThan(0);
            RuleFor(c => c.CompanyName).NotEmpty().MinimumLength(2);
            RuleFor(c => c.TaxNo).NotEmpty().MinimumLength(2);
        }
    }
}
