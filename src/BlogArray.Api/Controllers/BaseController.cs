using BlogArray.Domain.Constants;
using BlogArray.Domain.DTOs;
using BlogArray.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace BlogArray.Api.Controllers;

/// <summary>
/// Base Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    /// <summary>
    /// Gets the ID of the logged-in user from the claims.
    /// </summary>
    /// <remarks>
    /// This retrieves the user ID from the `NameIdentifier` claim. If the claim is not present, it returns 0 by default.
    /// </remarks>
    protected int LoggedInUserID => Converter.ToInt(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, 0);

    /// <summary>
    /// Gets the email of the logged-in user from the claims.
    /// </summary>
    /// <remarks>
    /// This retrieves the user email from the `Email` claim. If the claim is not present, it returns an empty string by default.
    /// </remarks>
    protected string LoggedInUserEmail => Converter.ToString(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value);

    /// <summary>
    /// Gets the current role of the logged-in user from the claims.
    /// </summary>
    /// <remarks>
    /// This retrieves the user role from the `Role` claim. If the claim is not present, it returns an empty string by default.
    /// </remarks>
    protected string LoggedInUserRole => Converter.ToString(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value);

    /// <summary>
    /// Checks if the user can publish post
    /// </summary>
    public bool UserCanPublish => new List<string>() { RoleConstants.Admin, RoleConstants.Editor }.Contains(LoggedInUserRole);

    /// <summary>
    /// Creates a response indicating that the model state is invalid.
    /// </summary>
    /// <param name="ModelState">The model state containing validation errors.</param>
    /// <returns>An <see cref="IActionResult"/> containing the validation error details.</returns>
    /// <remarks>
    /// This method collects all the validation errors from the <paramref name="ModelState"/> and returns a 400 Bad Request response with the error messages.
    /// </remarks>
    protected IActionResult ModelStateError(ModelStateDictionary ModelState)
    {
        string[] errors = ModelState.Values.SelectMany(state => state.Errors).Select(error => error.ErrorMessage).ToArray();
        return ErrorDetails.BadRequest("Validation Error", "Invalid input data.", errors);
    }

    /// <summary>
    /// Returns a success response with the specified result.
    /// </summary>
    /// <typeparam name="T">The type of the result being returned.</typeparam>
    /// <param name="result">The result object to include in the response.</param>
    /// <returns>An <see cref="IActionResult"/> containing the result with an HTTP 200 OK status.</returns>
    /// <remarks>
    /// This method wraps the provided result in an HTTP 200 OK response, indicating that the request was successful.
    /// </remarks>
    protected IActionResult Success<T>(T result)
    {
        return Ok(result);
    }
}
