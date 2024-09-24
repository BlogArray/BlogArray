using BlogArray.Application.Features.Media.Queries;
using BlogArray.Domain.Constants;
using BlogArray.Domain.DTOs;
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
public class MediaController(IMediator mediatr) : BaseController
{
    /// <summary>
    /// Retrieves a paginated list of all media/assets with optional search functionality.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve (default is 1).</param>
    /// <param name="pageSize">The number of media/assets per page (default is 10).</param>
    /// <param name="searchTerm">
    /// Optional search term to filter media/assets by their name.
    /// </param>
    /// <returns>
    /// A paginated list of media/assets, optionally filtered by the search term, wrapped in an <see cref="IActionResult"/>.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<MediaInfo>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> GetAllMediaAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null)
    {
        PagedResult<MediaInfo> media = await mediatr.Send(new GetMediaQuery(pageNumber, pageSize, searchTerm));

        return Ok(media);
    }


}
