﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SimpleWebApi.Exceptions
{
    public class GlobalExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            //Add TraceIdentifier to exception data for logging.
            exception.Data.Add("TraceID", httpContext.TraceIdentifier);
            logger.LogError(exception, "Unhandled exception occurred");
            httpContext.Response.StatusCode = exception switch
            {
                ApplicationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Type = exception.GetType().Name,
                    Title = "An error occurred while processing your request",
                    Detail = exception.Message
                }
            });
        }
    }
}
