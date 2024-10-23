using BlogArray.Api.Middleware;
using BlogArray.Application.Features.Media.Commands;
using BlogArray.Application.Features.Media.Queries;
using BlogArray.Domain.Constants;
using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

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
    /// <param name="assetType">Asset type.</param>
    /// <returns>
    /// A paginated list of media/assets, optionally filtered by the search term, wrapped in an <see cref="IActionResult"/>.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<MediaInfo>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> GetAllMediaAsync(int pageNumber = 1, int pageSize = 10, AssetType? assetType = null, string? searchTerm = null)
    {
        PagedResult<MediaInfo> media = await mediatr.Send(new GetMediaQuery(pageNumber, pageSize, searchTerm, assetType));

        return Ok(media);
    }

    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> PostFile([FromForm] List<IFormFile> files)
    {
        ReturnResult<string[]> result = await mediatr.Send(new UploadMediaCommand(files));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message, result.Result) : Ok(result.Result);
    }
}
