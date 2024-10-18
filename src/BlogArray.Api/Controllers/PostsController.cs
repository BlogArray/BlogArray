using BlogArray.Application.Features.Posts.Commands;
using BlogArray.Application.Features.Posts.Queries;
using BlogArray.Domain.Constants;
using BlogArray.Domain.DTOs;
using BlogArray.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogArray.Api.Controllers;

/// <summary>
/// Posts API Controller handles actions related to blog posts.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PostsController(IMediator mediatr) : BaseController
{
    /// <summary>
    /// Gets a post by its ID for public viewing. Published posts are accessible to everyone.
    /// Unpublished posts are only accessible by Admin, Editor, or the post's Author.
    /// </summary>
    /// <param name="slug">The ID of the post.</param>
    /// <returns>The requested post if found and accessible, otherwise 404 or 403.</returns>
    [HttpGet("{slug}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPostAsync(string slug)
    {
        PostDTO? post = await mediatr.Send(new GetPostForPublicQuery(slug, User.Identity.IsAuthenticated));
        return post == null ? PostErrors.SlugNotFound(slug) : Ok(post);
    }

    /// <summary>
    /// Creates a new post. Only Admin, Editor, Author, and Contributor roles can create posts.
    /// Contributors can only save drafts.
    /// </summary>
    /// <param name="createPost">The data transfer object containing the post details.</param>
    /// <returns>The ID of the created post if successful.</returns>
    [Authorize(Roles = RoleConstants.AdminEditorAuthor)]
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReturnResult<int>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostDTO createPost)
    {
        if (!ModelState.IsValid)
        {
            return ModelStateError(ModelState);
        }

        ReturnResult<int> result = await mediatr.Send(new CreatePostCommand(createPost, LoggedInUserID, UserCanPublish));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result);
    }

    /// <summary>
    /// Gets a post by its ID for editing. Only Admin, Editor, or the Author of the post
    /// can access this endpoint.
    /// </summary>
    /// <param name="id">The ID of the post.</param>
    /// <returns>The post in a format suitable for editing if authorized, otherwise 404 or 403.</returns>
    [HttpGet("edit/{id}")]
    [Authorize(Roles = RoleConstants.AdminEditorAuthor)]
    public async Task<IActionResult> GetPostForEditing(int id)
    {
        EditPostDTO? post = await mediatr.Send(new GetPostForEditingQuery(id, User, LoggedInUserID));
        return post == null ? PostErrors.NotFound(id) : Ok(post);
    }

    /// <summary>
    /// Updates an existing post. Only Admin, Editor, or the Author of the post can update it.
    /// </summary>
    /// <param name="id">The ID of the post.</param>
    /// <param name="editPost">The data transfer object containing the updated post details.</param>
    /// <returns>200 OK if successful, 404 if the post is not found, or 403 if unauthorized.</returns>
    [Authorize(Roles = RoleConstants.AdminEditorAuthor)]
    [HttpPut("update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReturnResult<int>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> UpdatePostAsync(int id, [FromBody] EditPostDTO editPost)
    {
        if (!ModelState.IsValid)
        {
            return ModelStateError(ModelState);
        }

        ReturnResult<int> result = await mediatr.Send(new EditPostCommand(id, editPost, LoggedInUserID, User, UserCanPublish));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result);
    }

    /// <summary>
    /// Deletes an existing post by ID. Only Admin, Editor, or the Author of the post can delete it.
    /// </summary>
    /// <param name="id">The ID of the post to be deleted.</param>
    /// <returns>200 OK if successful, 404 if the post is not found, or 403 if unauthorized.</returns>
    [HttpDelete("delete/{id}")]
    [Authorize(Roles = RoleConstants.AdminEditorAuthor)]
    public async Task<IActionResult> DeletePost(int id)
    {
        ReturnResult<int> result = await mediatr.Send(new DeletePostCommand(id, LoggedInUserID, User));
        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Message);
    }
}
