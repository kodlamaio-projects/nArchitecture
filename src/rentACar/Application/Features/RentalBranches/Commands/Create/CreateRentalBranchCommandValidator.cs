using FluentValidation;

namespace Application.Features.RentalBranches.Commands.Create;

public class CreateRentalBranchCommandValidator : AbstractValidator<CreateRentalBranchCommand>
{
    public CreateRentalBranchCommandValidator()
    {
        RuleFor(c => c.City).NotEmpty();
    }
}
