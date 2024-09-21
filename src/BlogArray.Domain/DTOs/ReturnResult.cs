using Microsoft.AspNetCore.Mvc;

namespace BlogArray.Domain.DTOs;

public class ReturnResult<T> : ReturnResult
{
    public T? Result { get; set; }
}

public class ReturnResult
{
    private int _code;

    /// <summary>
    /// The HTTP status code. Automatically sets <see cref="Status"/> to true if the code is 200.
    /// </summary>
    public int Code
    {
        get => _code;
        set
        {
            _code = value;
            Status = _code == 200;
        }
    }

    /// <summary>
    /// Indicates whether the operation was successful. This is automatically set to true if <see cref="Code"/> is 200.
    /// </summary>
    public bool Status { get; private set; }

    /// <summary>
    /// Title of the return result.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// The message detailing the result of the operation.
    /// </summary>
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
    /// <param name="errors">An optional array of specific error messages.</param>
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
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the detailed description of the error.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets an optional array of specific error messages.
    /// </summary>
    public string[]? Errors { get; set; }

    /// <summary>
    /// Creates an error response based on the provided status code.
    /// </summary>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <param name="title">A short title describing the status.</param>
    /// <param name="description">A detailed description of the status.</param>
    /// <param name="errors">An optional array of specific error messages.</param>
    /// <returns>An <see cref="IActionResult"/> representing the response.</returns>
    public static IActionResult CreateResponse(int statusCode, string title, string description, string[]? errors = null)
    {
        return new ObjectResult(new ErrorDetails(statusCode, title, description, errors))
        {
            StatusCode = statusCode
        };
    }

    /// <summary>
    /// Returns a success response (HTTP 200).
    /// </summary>
    public static IActionResult Success(string title, string description, string[]? errors = null)
    {
        return CreateResponse(200, title, description, errors);
    }

    /// <summary>
    /// Returns a bad request response (HTTP 400).
    /// </summary>
    public static IActionResult BadRequest(string title, string description, string[]? errors = null)
    {
        return CreateResponse(400, title, description, errors);
    }

    /// <summary>
    /// Returns an unauthorized response (HTTP 401).
    /// </summary>
    public static IActionResult Unauthorized(string title, string description, string[]? errors = null)
    {
        return CreateResponse(401, title, description, errors);
    }

    /// <summary>
    /// Returns a forbidden response (HTTP 403).
    /// </summary>
    public static IActionResult Forbidden(string title, string description, string[]? errors = null)
    {
        return CreateResponse(403, title, description, errors);
    }

    /// <summary>
    /// Returns a not found response (HTTP 404).
    /// </summary>
    public static IActionResult NotFound(string title, string description, string[]? errors = null)
    {
        return CreateResponse(404, title, description, errors);
    }

    /// <summary>
    /// Returns a conflict response (HTTP 409).
    /// </summary>
    public static IActionResult Conflict(string title, string description, string[]? errors = null)
    {
        return CreateResponse(409, title, description, errors);
    }

    /// <summary>
    /// Returns an internal server error response (HTTP 500).
    /// </summary>
    public static IActionResult InternalServerError(string title, string description, string[]? errors = null)
    {
        return CreateResponse(500, title, description, errors);
    }

    /// <summary>
    /// Creates a custom error response.
    /// </summary>
    public static IActionResult CreateError(int code, string title, string description, string[]? errors = null)
    {
        return CreateResponse(code, title, description, errors);
    }
}
