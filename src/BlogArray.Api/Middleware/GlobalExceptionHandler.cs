using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BlogArray.Api.Middleware;

/// <summary>
/// A global exception handler to handle uncaught exceptions in the application and return a standardized response.
/// </summary>
/// <remarks>
/// Constructor for the GlobalExceptionHandler.
/// </remarks>
/// <param name="logger">An instance of <see cref="ILogger{GlobalExceptionHandler}"/> to log exception details.</param>
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{

    /// <summary>
    /// Tries to handle an exception and returns an appropriate response.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="exception">The exception to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous exception handling. Returns true when the exception is handled.</returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        // Log the exception details
        logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

        // Create problem details to return to the client
        ProblemDetails problemDetails = new()
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
            Detail = "An unexpected error occurred. Please try again later.",
            Instance = httpContext.Request.Path
        };

        // Set response status code
        httpContext.Response.StatusCode = problemDetails.Status.Value;

        // Write problem details as JSON response
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
