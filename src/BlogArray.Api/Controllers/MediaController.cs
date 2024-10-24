using BlogArray.Application.Features.Media.Commands;
using BlogArray.Application.Features.Media.Queries;
using BlogArray.Domain.Constants;
using BlogArray.Domain.DTOs;
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
    /// <param name="assetType">Asset type to filter the media/assets by.</param>
    /// <returns>
    /// A paginated list of media/assets, optionally filtered by the search term/>.
    /// Returns a 200 OK status with the result on success,
    /// a 404 Not Found status if no media/assets are found, 
    /// or a 400 Bad Request status if the request is invalid.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<MediaInfo>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> GetAllMediaAsync(int pageNumber = 1, int pageSize = 10, AssetType? assetType = null, string? searchTerm = null)
    {
        PagedResult<MediaInfo> media = await mediatr.Send(new GetMediaQuery(pageNumber, pageSize, searchTerm, assetType));

        return Ok(media);
    }

    /// <summary>
    /// Uploads media files.
    /// </summary>
    /// <param name="files">The list of files to be uploaded.</param>
    /// <returns>
    /// Returns a 200 OK status with message if the upload is successful, 
    /// or an appropriate error response (e.g., 400 Bad Request) if the upload fails.
    /// The response includes a message indicating the result of the operation.
    /// </returns>
    [HttpPost]
    [Route("upload")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> PostFile([FromForm] List<IFormFile> files)
    {
        ReturnResult<string[]> result = await mediatr.Send(new UploadMediaCommand(files, LoggedInUserID));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message, result.Result) : Ok(result.Message);
    }

    /// <summary>
    /// Deletes the specified media files.
    /// </summary>
    /// <param name="files">A list of file IDs representing the media files to be deleted.</param>
    /// <returns>
    /// Returns a 200 OK status with message if the deletion is successful, 
    /// or an appropriate error response (e.g., 400 Bad Request) if the deletion fails.
    /// The response includes a message indicating the result of the operation.
    /// </returns>
    [HttpDelete]
    [Route("delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Delete(List<int> files)
    {
        ReturnResult<int> result = await mediatr.Send(new DeleteMediaCommand(files, LoggedInUserID, false));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Message);
    }

}
