using Microsoft.AspNetCore.Mvc;

namespace BlogArray.Domain.DTOs;

public class ReturnResult<T> : ReturnResult
{
    public required T Result { get; set; }
}

public class ReturnResult
{
    public bool Status { get; set; } = false;

    public required string Message { get; set; }
}

/// <summary>
/// Represents detailed error information, including HTTP status code, title, description, and optional error details.
/// </summary>
public class ErrorDetails
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorDetails"/> class with the specified parameters.
    /// </summary>
    /// <param name="status">The HTTP status code associated with the error.</param>
    /// <param name="title">A short title describing the error.</param>
    /// <param name="description">A detailed description of the error.</param>
    /// <param name="errors">An array of specific error messages, if applicable.</param>
    private ErrorDetails(int status, string title, string description, string[]? errors = null)
    {
        Status = status;
        Title = title;
        Description = description;
        Errors = errors;
    }

    /// <summary>
    /// Gets or sets the HTTP status code associated with the error.
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Gets or sets the short title describing the error.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the detailed description of the error.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets an optional array of specific error messages.
    /// </summary>
    public string[]? Errors { get; set; }

    /// <summary>
    /// Returns a success response (HTTP 200).
    /// </summary>
    /// <param name="title">A short title describing the success.</param>
    /// <param name="description">A detailed description of the success.</param>
    /// <param name="errors">An optional array of specific messages.</param>
    /// <returns>An <see cref="IActionResult"/> representing a success response.</returns>
    public static IActionResult Success(string title, string description, string[]? errors = null)
    {
        return new ObjectResult(new ErrorDetails(200, title, description, errors))
        {
            StatusCode = 200
        };
    }

    /// <summary>
    /// Returns a bad request response (HTTP 400).
    /// </summary>
    /// <param name="title">A short title describing the error.</param>
    /// <param name="description">A detailed description of the error.</param>
    /// <param name="errors">An optional array of specific error messages.</param>
    /// <returns>An <see cref="IActionResult"/> representing a bad request error.</returns>
    public static IActionResult BadRequest(string title, string description, string[]? errors = null)
    {
        return new ObjectResult(new ErrorDetails(400, title, description, errors))
        {
            StatusCode = 400
        };
    }

    /// <summary>
    /// Returns an unauthorized response (HTTP 401).
    /// </summary>
    /// <param name="title">A short title describing the error.</param>
    /// <param name="description">A detailed description of the error.</param>
    /// <param name="errors">An optional array of specific error messages.</param>
    /// <returns>An <see cref="IActionResult"/> representing an unauthorized error.</returns>
    public static IActionResult Unauthorized(string title, string description, string[]? errors = null)
    {
        return new ObjectResult(new ErrorDetails(401, title, description, errors))
        {
            StatusCode = 401
        };
    }

    /// <summary>
    /// Returns a forbidden response (HTTP 403).
    /// </summary>
    /// <param name="title">A short title describing the error.</param>
    /// <param name="description">A detailed description of the error.</param>
    /// <param name="errors">An optional array of specific error messages.</param>
    /// <returns>An <see cref="IActionResult"/> representing a forbidden error.</returns>
    public static IActionResult Forbidden(string title, string description, string[]? errors = null)
    {
        return new ObjectResult(new ErrorDetails(403, title, description, errors))
        {
            StatusCode = 403
        };
    }

    /// <summary>
    /// Returns a not found response (HTTP 404).
    /// </summary>
    /// <param name="title">A short title describing the error.</param>
    /// <param name="description">A detailed description of the error.</param>
    /// <param name="errors">An optional array of specific error messages.</param>
    /// <returns>An <see cref="IActionResult"/> representing a not found error.</returns>
    public static IActionResult NotFound(string title, string description, string[]? errors = null)
    {
        return new ObjectResult(new ErrorDetails(404, title, description, errors))
        {
            StatusCode = 404
        };
    }

    /// <summary>
    /// Returns a not found response (HTTP 409).
    /// </summary>
    /// <param name="title">A short title describing the error.</param>
    /// <param name="description">A detailed description of the error.</param>
    /// <param name="errors">An optional array of specific error messages.</param>
    /// <returns>An <see cref="IActionResult"/> representing a not found error.</returns>
    public static IActionResult Conflict(string title, string description, string[]? errors = null)
    {
        return new ObjectResult(new ErrorDetails(409, title, description, errors))
        {
            StatusCode = 409
        };
    }

    /// <summary>
    /// Returns an internal server error response (HTTP 500).
    /// </summary>
    /// <param name="title">A short title describing the error.</param>
    /// <param name="description">A detailed description of the error.</param>
    /// <param name="errors">An optional array of specific error messages.</param>
    /// <returns>An <see cref="IActionResult"/> representing an internal server error.</returns>
    public static IActionResult InternalServerError(string title, string description, string[]? errors = null)
    {
        return new ObjectResult(new ErrorDetails(500, title, description, errors))
        {
            StatusCode = 500
        };
    }
}
