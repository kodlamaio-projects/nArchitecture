﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

internal class InternalServerErrorProblemDetails : ProblemDetails
{
    public InternalServerErrorProblemDetails(string detail)
    {
        Title = "Internal server error";
        Detail = detail;
        Status = StatusCodes.Status500InternalServerError;
        Type = "https://example.com/probs/internal";
        Instance = "";
    }
}
