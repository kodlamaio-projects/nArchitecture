using System.Net;

namespace Core.CrossCuttingConcerns.Extensions
{
    public static class HttpStatusCodeExtensions
    {
        public static int GetCode(this HttpStatusCode httpStatusCode) => (int)httpStatusCode;
    }
}
