using FluentValidation;

namespace Application.Features.RentalBranches.Commands.Update;

public class UpdateRentalBranchCommandValidator : AbstractValidator<UpdateRentalBranchCommand>
{
    public UpdateRentalBranchCommandValidator()
    {
        RuleFor(c => c.City).NotEmpty();
    }
}
