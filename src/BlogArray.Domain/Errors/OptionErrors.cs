using BlogArray.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlogArray.Domain.Errors;

public static class OptionErrors
{
    /// <summary>
    /// Returns a not found error response when an item with the given key is not found.
    /// </summary>
    /// <param name="key">The key of the item that was not found.</param>
    /// <returns>An <see cref="IActionResult"/> representing the item not found error.</returns>
    public static IActionResult NotFound(string key)
    {
        return ErrorDetails.NotFound("Option.NotFound", $"The option with key '{key}' was not found.");
    }


}
