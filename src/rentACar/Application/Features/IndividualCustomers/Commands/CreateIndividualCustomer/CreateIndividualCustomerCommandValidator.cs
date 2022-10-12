using FluentValidation;

namespace Application.Features.IndividualCustomers.Commands.CreateIndividualCustomer
{
    public class CreateIndividualCustomerCommandValidator : AbstractValidator<CreateIndividualCustomerCommand>
    {
        public CreateIndividualCustomerCommandValidator()
        {
            RuleFor(c => c.CustomerId).GreaterThan(0);
            RuleFor(c => c.FirstName).NotEmpty().MinimumLength(2);
            RuleFor(c => c.LastName).NotEmpty().MinimumLength(2);
            RuleFor(c => c.NationalIdentity).NotEmpty().MinimumLength(11).MaximumLength(11);
        }
    }
}
