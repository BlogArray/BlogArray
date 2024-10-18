using BlogArray.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlogArray.Domain.Errors;

public static class PostErrors
{
    /// <summary>
    /// Returns a not found error response when a post with the given id is not found.
    /// </summary>
    /// <param name="id">The id of the post that was not found.</param>
    /// <returns>An <see cref="IActionResult"/> representing the post not found error.</returns>
    public static IActionResult NotFound(int id)
    {
        return ErrorDetails.NotFound("Post.NotFound", $"The post with id '{id}' was not found.");
    }

    /// <summary>
    /// Returns a not found error response when a post with the given name is not found.
    /// </summary>
    /// <param name="name">The name of the post that was not found.</param>
    /// <returns>An <see cref="IActionResult"/> representing the post not found error.</returns>
    public static IActionResult SlugNotFound(string name)
    {
        return ErrorDetails.NotFound("Post.NotFound", $"The post with name '{name}' was not found.");
    }
}
