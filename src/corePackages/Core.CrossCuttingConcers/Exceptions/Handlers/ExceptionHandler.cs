using FluentValidation;

namespace Core.CrossCuttingConcerns.Exceptions.Handlers
{
    public abstract class ExceptionHandler
    {
        public Task HandleExceptionAsync(Exception exception)
        {
            return exception switch
            {
                _ when exception is ValidationException => HandleException(exception),
                _ when exception is BusinessException => HandleException(exception),
                _ when exception is NotFoundException => HandleException(exception),
                _ when exception is AuthorizationException => HandleException(exception),
                _ => HandleException(exception)
            };
        }

        protected abstract Task HandleException(BusinessException businessException);
        protected abstract Task HandleException(ValidationException validationException);
        protected abstract Task HandleException(AuthorizationException authorizationException);
        protected abstract Task HandleException(NotFoundException notFoundException);
        protected abstract Task HandleException(Exception exception);
    }
}
