using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Core.Application.Pipelines.Validation;

public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
                                  RequestHandlerDelegate<TResponse> next)
    {
        ValidationContext<object> context = new(request);
        List<ValidationFailure> failures = _validators
                                           .Select(validator => validator.Validate(context))
                                           .SelectMany(result => result.Errors)
                                           .Where(failure => failure != null)
                                           .ToList();
        if (failures.Count != 0) throw new ValidationException(failures);
        return next();
    }
}