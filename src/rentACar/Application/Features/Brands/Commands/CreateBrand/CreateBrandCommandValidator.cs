using FluentValidation;

namespace Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MinimumLength(2);
    }
}