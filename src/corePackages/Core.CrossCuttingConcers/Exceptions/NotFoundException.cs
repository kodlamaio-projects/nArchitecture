using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class NotFoundException : BusinessException
    {
        public NotFoundException(string message) : base(message, Enums.BusinessExceptionTypes.NotFound)
        {
        }
    }
}
