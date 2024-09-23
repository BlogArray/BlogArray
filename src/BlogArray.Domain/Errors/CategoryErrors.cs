using BlogArray.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlogArray.Domain.Errors;

public static class CategoryErrors
{
    /// <summary>
    /// Returns a not found error response when a category with the given id is not found.
    /// </summary>
    /// <param name="id">The id of the category that was not found.</param>
    /// <returns>An <see cref="IActionResult"/> representing the category not found error.</returns>
    public static IActionResult NotFound(int id)
    {
        return ErrorDetails.NotFound("Category.NotFound", $"The category with id '{id}' was not found.");
    }

    /// <summary>
    /// Returns a not found error response when a category with the given name is not found.
    /// </summary>
    /// <param name="name">The name of the category that was not found.</param>
    /// <returns>An <see cref="IActionResult"/> representing the category not found error.</returns>
    public static IActionResult SlugNotFound(string name)
    {
        return ErrorDetails.NotFound("Category.NotFound", $"The category with name '{name}' was not found.");
    }

}
