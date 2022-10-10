using FluentValidation;

namespace Application.Features.Models.Commands.CreateModel;

public class CreateModelCommandValidator : AbstractValidator<CreateModelCommand>
{
    public CreateModelCommandValidator()
    {
        RuleFor(c => c.Name)
            .MinimumLength(2);
        RuleFor(c => c.DailyPrice).GreaterThan(0);
    }
}