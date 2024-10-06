using BlogArray.Domain.Constants;
using BlogArray.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogArray.Api.Controllers;

/// <summary>
/// Controller responsible for media/asset related actions.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = RoleConstants.AdminEditorAuthor)]
public class PostsController(IMediator mediatr) : BaseController
{
    public async Task<IActionResult> GetAllMediaAsync(int pageNumber = 1, int pageSize = 10, PostType? postType = null, string? searchTerm = null)
    {
        return Ok();
    }


}
