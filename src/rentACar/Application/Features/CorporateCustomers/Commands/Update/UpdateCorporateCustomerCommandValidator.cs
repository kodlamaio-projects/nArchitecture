using FluentValidation;

namespace Application.Features.CorporateCustomers.Commands.Update;

public class UpdateCorporateCustomerCommandValidator : AbstractValidator<UpdateCorporateCustomerCommand>
{
    public UpdateCorporateCustomerCommandValidator()
    {
        RuleFor(c => c.CustomerId).GreaterThan(0);
        RuleFor(c => c.CompanyName).NotEmpty().MinimumLength(2);
        RuleFor(c => c.TaxNo).NotEmpty().MinimumLength(2);
    }
}
