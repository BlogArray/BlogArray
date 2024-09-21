using BlogArray.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlogArray.Domain.Errors;

public static class UserErrors
{
    /// <summary>
    /// Returns a not found error response when a user with the given Id is not found.
    /// </summary>
    /// <param name="id">The user Id that was not found.</param>
    /// <returns>An <see cref="IActionResult"/> representing the user not found error.</returns>
    public static IActionResult NotFound(int id) => ErrorDetails.NotFound("User.NotFound", $"The user with Id '{id}' was not found.");

    /// <summary>
    /// Returns a not found error response when a user with the given email is not found.
    /// </summary>
    /// <param name="email">The email address of the user that was not found.</param>
    /// <returns>An <see cref="IActionResult"/> representing the user not found error.</returns>
    public static IActionResult EmailNotFound(string email) => ErrorDetails.NotFound("User.EmailNotFound", $"The user with email '{email}' was not found.");

    /// <summary>
    /// Returns a conflict error response when a user with the given email already exists.
    /// </summary>
    /// <param name="email">The email address that already exists.</param>
    /// <returns>An <see cref="IActionResult"/> representing the email already exists error.</returns>
    public static IActionResult EmailExists(string email) => ErrorDetails.Conflict("User.EmailExists", $"The user with email '{email}' already exists.");

    /// <summary>
    /// Returns a conflict error response when a user with the given username already exists.
    /// </summary>
    /// <param name="username">The username that already exists.</param>
    /// <returns>An <see cref="IActionResult"/> representing the username already exists error.</returns>
    public static IActionResult UsernameExists(string username) => ErrorDetails.Conflict("User.UsernameExists", $"The user with username '{username}' already exists.");

    /// <summary>
    /// Returns an unauthorized error response when the user is not authorized to perform the action.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> representing the unauthorized error.</returns>
    public static IActionResult Invalid() => ErrorDetails.Unauthorized("User.Invalid", "Invalid username or password.");

    /// <summary>
    /// Returns an unauthorized error response when the user is not authorized to perform the action.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> representing the unauthorized error.</returns>
    public static IActionResult UserLocked() => ErrorDetails.Unauthorized("User.Lockedout", "User account is locked for additional security.");

    /// <summary>
    /// Returns an unauthorized error response when the user is not authorized to perform the action.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> representing the unauthorized error.</returns>
    public static IActionResult Unauthorized() => ErrorDetails.Unauthorized("User.Unauthorized", "You are not authorized to perform this action.");

    /// <summary>
    /// Returns a forbidden error response when the user does not have permission to access the resource.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> representing the forbidden error.</returns>
    public static IActionResult Forbidden() => ErrorDetails.Forbidden("User.Forbidden", "You do not have permission to access this resource.");

    /// <summary>
    /// Returns an internal server error response when an unexpected error occurs.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> representing the internal server error.</returns>
    public static IActionResult InternalServerError() => ErrorDetails.InternalServerError("User.InternalServerError", "An unexpected error occurred. Please try again later.");

    /// <summary>
    /// Returns a bad request error response when the user input is invalid.
    /// </summary>
    /// <param name="description">The detailed description of why the input is invalid.</param>
    /// <returns>An <see cref="IActionResult"/> representing the bad request error.</returns>
    public static IActionResult BadRequest(string description) => ErrorDetails.BadRequest("User.BadRequest", description);

}
