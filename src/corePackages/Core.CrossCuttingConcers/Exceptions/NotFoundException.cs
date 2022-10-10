using Core.CrossCuttingConcerns.Enums;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class NotFoundException : BusinessException
    {
        public NotFoundException(string message) : base(message, BusinessExceptionTypes.NotFound)
        {
        }
    }
}
