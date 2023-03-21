using FluentValidation;

namespace Application.Features.UserOperationClaims.Commands.Update;

public class UpdateUserOperationClaimCommandValidator : AbstractValidator<UpdateUserOperationClaimCommand>
{
    public UpdateUserOperationClaimCommandValidator()
    {
        RuleFor(c => c.UserId).GreaterThan(0);
        RuleFor(c => c.OperationClaimId).GreaterThan(0);
    }
}
