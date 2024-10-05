using BlogArray.Application.Features.AppOptions.Commands;
using BlogArray.Application.Features.Settings.Queries;
using BlogArray.Domain.Constants;
using BlogArray.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BlogArray.Api.Controllers;

/// <summary>
/// API controller for managing application settings.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = RoleConstants.Admin)]
public class SettingsController(IMediator mediatr) : BaseController
{
    /// <summary>
    /// Retrieves the site information.
    /// </summary>
    /// <returns>Returns the <see cref="BlogArray.Domain.DTOs.SiteInfo"/> if found; otherwise, returns a 404 Not Found or 400 Bad Request.</returns>
    [HttpGet("siteinfo")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SiteInfo))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> SiteInfo()
    {
        SiteInfo? option = await mediatr.Send(new GetSiteInfoQuery());
        return option == null ? NotFound() : Ok(option);
    }

    /// <summary>
    /// Updates or creates the site information.
    /// </summary>
    /// <param name="siteInfo">The <see cref="BlogArray.Domain.DTOs.SiteInfo"/> object to be saved or updated.</param>
    /// <returns>Returns the status of the operation or an error response.</returns>
    [HttpPost("siteinfo")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> SiteInfo([FromBody] SiteInfo siteInfo)
    {
        if (!ModelState.IsValid)
        {
            return ModelStateError(ModelState);
        }

        ReturnResult<int> result = await mediatr.Send(new CreateOrUpdateOptionCommand(new AppOptionsBase
        {
            AutoLoad = true,
            Key = "SiteInfo",
            Value = JsonSerializer.Serialize(siteInfo)
        }));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Result);
    }

    /// <summary>
    /// Retrieves the email configuration.
    /// </summary>
    /// <returns>Returns the <see cref="EmailSettings"/> if found; otherwise, returns a 404 Not Found or 400 Bad Request.</returns>
    [HttpGet("email")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmailSettings))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Email()
    {
        EmailSettings? option = await mediatr.Send(new GetEmailSettingsQuery());
        return option == null ? NotFound() : Ok(option);
    }

    /// <summary>
    /// Updates or creates the email configuration.
    /// </summary>
    /// <param name="sMTPOptions">The <see cref="EmailSettings"/> object to be saved or updated.</param>
    /// <returns>Returns the status of the operation or an error response.</returns>
    [HttpPost("email")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Email([FromBody] EmailSettings sMTPOptions)
    {
        if (!ModelState.IsValid)
        {
            return ModelStateError(ModelState);
        }

        ReturnResult<int> result = await mediatr.Send(new CreateOrUpdateOptionCommand(new AppOptionsBase
        {
            AutoLoad = true,
            Key = "SMTP",
            Value = JsonSerializer.Serialize(sMTPOptions)
        }));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Result);
    }

    /// <summary>
    /// Retrieves the content options.
    /// </summary>
    /// <returns>Returns the <see cref="ContentSettings"/> if found; otherwise, returns a 404 Not Found or 400 Bad Request.</returns>
    [HttpGet("content")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContentSettings))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Content()
    {
        ContentSettings? option = await mediatr.Send(new GetContentOptionsQuery());
        return option == null ? NotFound() : Ok(option);
    }

    /// <summary>
    /// Updates or creates the content options.
    /// </summary>
    /// <param name="contentOptions">The <see cref="ContentSettings"/> object to be saved or updated.</param>
    /// <returns>Returns the status of the operation or an error response.</returns>
    [HttpPost("content")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Content([FromBody] ContentSettings contentOptions)
    {
        if (!ModelState.IsValid)
        {
            return ModelStateError(ModelState);
        }

        ReturnResult<int> result = await mediatr.Send(new CreateOrUpdateOptionCommand(new AppOptionsBase
        {
            AutoLoad = true,
            Key = "Content",
            Value = JsonSerializer.Serialize(contentOptions)
        }));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Result);
    }

    /// <summary>
    /// Retrieves the media settings.
    /// </summary>
    /// <returns>Returns the <see cref="MediaSettings"/> if found; otherwise, returns a 404 Not Found or 400 Bad Request.</returns>
    [HttpGet("media")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MediaSettings))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Media()
    {
        MediaSettings? option = await mediatr.Send(new GetMediaSettingsQuery());
        return option == null ? NotFound() : Ok(option);
    }

    /// <summary>
    /// Updates or creates the media settings.
    /// </summary>
    /// <param name="mediaOptions">The <see cref="MediaSettings"/> object to be saved or updated.</param>
    /// <returns>Returns the status of the operation or an error response.</returns>
    [HttpPost("media")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Media([FromBody] MediaSettings mediaOptions)
    {
        if (!ModelState.IsValid)
        {
            return ModelStateError(ModelState);
        }

        ReturnResult<int> result = await mediatr.Send(new CreateOrUpdateOptionCommand(new AppOptionsBase
        {
            AutoLoad = true,
            Key = "Media",
            Value = JsonSerializer.Serialize(mediaOptions)
        }));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Result);
    }

    /// <summary>
    /// Retrieves the comment settings.
    /// </summary>
    /// <returns>Returns the <see cref="CommentSettings"/> if found; otherwise, returns a 404 Not Found or 400 Bad Request.</returns>
    [HttpGet("comment")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentSettings))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Comment()
    {
        CommentSettings? option = await mediatr.Send(new GetCommentSettingsQuery());
        return option == null ? NotFound() : Ok(option);
    }

    /// <summary>
    /// Updates or creates the comment settings.
    /// </summary>
    /// <param name="commentSettings">The <see cref="CommentSettings"/> object to be saved or updated.</param>
    /// <returns>Returns the status of the operation or an error response.</returns>
    [HttpPost("comment")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Comment([FromBody] CommentSettings commentSettings)
    {
        if (!ModelState.IsValid)
        {
            return ModelStateError(ModelState);
        }

        ReturnResult<int> result = await mediatr.Send(new CreateOrUpdateOptionCommand(new AppOptionsBase
        {
            AutoLoad = true,
            Key = "Comments",
            Value = JsonSerializer.Serialize(commentSettings)
        }));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Result);
    }

}
