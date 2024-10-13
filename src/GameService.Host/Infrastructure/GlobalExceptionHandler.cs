using GameService.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GameService.Host.Infrastructure;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(
            exception,
            "Exception occurred: {Message}",
            exception.Message);

        ProblemDetails problemDetails = exception switch
        {
            ChoiceNotFoundException => new()
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not found",
                Detail = exception.Message
            },
            InvalidRandomNumberGeneratedException => new()
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server error",
                Detail = exception.Message
            },
            NoAvailableChoicesException => new()
            {
                Status = StatusCodes.Status204NoContent,
                Title = "No content",
                Detail = exception.Message
            },
            _ => new()
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server error",
            }
        };

        httpContext.Response.StatusCode = problemDetails.Status.GetValueOrDefault();

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
