using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class InternalServerProblemDetails : ProblemDetails
    {
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
