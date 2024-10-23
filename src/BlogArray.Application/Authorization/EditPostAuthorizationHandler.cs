using BlogArray.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BlogArray.Application.Authorization;

/// <summary>
/// Authorization handler to determine if a user has the permission to edit/delete a blog post.
/// </summary>
public class EditPostAuthorizationHandler : AuthorizationHandler<EditPostRequirement, int>
{
    /// <summary>
    /// Handles the authorization logic to determine if the current user can edit/delete the post.
    /// </summary>
    /// <param name="context">Authorization handler context that contains the user claims.</param>
    /// <param name="requirement">The edit post authorization requirement.</param>
    /// <param name="postAuthorId">The ID of the author who owns the post being edited.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditPostRequirement requirement, int postAuthorId)
    {
        ClaimsPrincipal user = context.User;

        // Check if the user has Admin or Editor role, allowing them to edit any post.
        if (user.IsInRole(RoleConstants.Admin) || user.IsInRole(RoleConstants.Editor))
        {
            context.Succeed(requirement); // Admin and Editor can edit all posts
        }
        // Check if the user is an Author or Contributor and if they own the post.
        else if (user.IsInRole(RoleConstants.Author) || user.IsInRole(RoleConstants.Contributor))
        {
            string? userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (postAuthorId.ToString() == userId)
            {
                context.Succeed(requirement); // Authors and Contributors can edit their own posts
            }
        }

        return Task.CompletedTask;
    }
}

/// <summary>
/// Represents the authorization requirement to edit/delete a post.
/// </summary>
public class EditPostRequirement : IAuthorizationRequirement { }
