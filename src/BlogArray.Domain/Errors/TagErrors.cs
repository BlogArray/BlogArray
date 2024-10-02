using BlogArray.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlogArray.Domain.Errors;

public static class TagErrors
{
    /// <summary>
    /// Returns a not found error response when a tag with the given id is not found.
    /// </summary>
    /// <param name="id">The id of the tag that was not found.</param>
    /// <returns>An <see cref="IActionResult"/> representing the tag not found error.</returns>
    public static IActionResult NotFound(int id)
    {
        return ErrorDetails.NotFound("Tag.NotFound", $"The tag with id '{id}' was not found.");
    }

    /// <summary>
    /// Returns a not found error response when a tag with the given name is not found.
    /// </summary>
    /// <param name="name">The name of the tag that was not found.</param>
    /// <returns>An <see cref="IActionResult"/> representing the tag not found error.</returns>
    public static IActionResult SlugNotFound(string name)
    {
        return ErrorDetails.NotFound("Tag.NotFound", $"The tag with name '{name}' was not found.");
    }

}
