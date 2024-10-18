using BlogArray.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BlogArray.Application.Authorization;

/// <summary>
/// 
/// </summary>
public class EditPostAuthorizationHandler : AuthorizationHandler<EditPostRequirement, int>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="requirement"></param>
    /// <param name="post"></param>
    /// <returns></returns>
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditPostRequirement requirement, int postAuthorId)
    {
        var user = context.User;

        if (user.IsInRole(RoleConstants.Admin) || user.IsInRole(RoleConstants.Editor))
        {
            context.Succeed(requirement); // Admin and Editor can edit all posts
        }
        else if (user.IsInRole(RoleConstants.Author) || user.IsInRole(RoleConstants.Contributor))
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (postAuthorId.ToString() == userId)
            {
                context.Succeed(requirement); // Authors and Contributors can edit their own posts
            }
        }

        return Task.CompletedTask;
    }
}

/// <summary>
/// 
/// </summary>
public class EditPostRequirement : IAuthorizationRequirement { }
