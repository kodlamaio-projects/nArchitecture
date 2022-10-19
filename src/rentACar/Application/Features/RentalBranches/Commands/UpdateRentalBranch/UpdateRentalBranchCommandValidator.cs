using FluentValidation;

namespace Application.Features.RentalBranches.Commands.UpdateRentalBranch
{
    public class UpdateRentalBranchCommandValidator : AbstractValidator<UpdateRentalBranchCommand>
    {
        public UpdateRentalBranchCommandValidator()
        {
            RuleFor(c => c.City).NotEmpty();
        }
    }
}
