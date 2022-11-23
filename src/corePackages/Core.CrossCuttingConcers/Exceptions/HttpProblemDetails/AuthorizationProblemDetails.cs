using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions;

internal class AuthorizationProblemDetails : ProblemDetails
{
    public AuthorizationProblemDetails(string detail)
    {
        Title = "Authorization error";
        Detail = detail;
        Status = StatusCodes.Status401Unauthorized;
        Type = "https://example.com/probs/authorization";
        Instance = "";
    }
}