using BlogArray.Application.Features.Posts.Commands;
using BlogArray.Domain.Constants;
using BlogArray.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogArray.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = RoleConstants.AdminEditorAuthor)]
public class PostsController(IMediator mediatr) : BaseController
{

    [Authorize(Roles = RoleConstants.AdminEditorAuthor)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReturnResult<int>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostDTO createPost)
    {
        if (!ModelState.IsValid)
        {
            return ModelStateError(ModelState);
        }

        ReturnResult<int> result = await mediatr.Send(new CreatePostCommand(createPost, LoggedInUserID));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result);
    }

    [Authorize(Roles = RoleConstants.AdminEditorAuthor)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReturnResult<int>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> UpdatePostAsync(int id, [FromBody] EditPostDTO editPost)
    {
        if (!ModelState.IsValid)
        {
            return ModelStateError(ModelState);
        }

        ReturnResult<int> result = await mediatr.Send(new EditPostCommand(id, editPost, LoggedInUserID));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result);
    }
}
