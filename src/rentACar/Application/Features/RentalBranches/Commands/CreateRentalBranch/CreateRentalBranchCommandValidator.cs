using FluentValidation;

namespace Application.Features.RentalBranches.Commands.CreateRentalBranch
{
    public class CreateRentalBranchCommandValidator : AbstractValidator<CreateRentalBranchCommand>
    {
        public CreateRentalBranchCommandValidator()
        {
            RuleFor(c => c.City).NotEmpty();
        }
    }
}
