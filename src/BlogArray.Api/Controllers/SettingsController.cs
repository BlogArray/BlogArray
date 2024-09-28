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
    /// <returns>Returns the <see cref="SMTPOptions"/> if found; otherwise, returns a 404 Not Found or 400 Bad Request.</returns>
    [HttpGet("email")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SMTPOptions))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Email()
    {
        SMTPOptions? option = await mediatr.Send(new GetEmailSettingsQuery());
        return option == null ? NotFound() : Ok(option);
    }

    /// <summary>
    /// Updates or creates the email configuration.
    /// </summary>
    /// <param name="sMTPOptions">The <see cref="SMTPOptions"/> object to be saved or updated.</param>
    /// <returns>Returns the status of the operation or an error response.</returns>
    [HttpPost("email")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Email([FromBody] SMTPOptions sMTPOptions)
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
    /// <returns>Returns the <see cref="ContentOptions"/> if found; otherwise, returns a 404 Not Found or 400 Bad Request.</returns>
    [HttpGet("content")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContentOptions))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Content()
    {
        ContentOptions? option = await mediatr.Send(new GetContentOptionsQuery());
        return option == null ? NotFound() : Ok(option);
    }

    /// <summary>
    /// Updates or creates the content options.
    /// </summary>
    /// <param name="contentOptions">The <see cref="ContentOptions"/> object to be saved or updated.</param>
    /// <returns>Returns the status of the operation or an error response.</returns>
    [HttpPost("content")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Content([FromBody] ContentOptions contentOptions)
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
    /// <returns>Returns the <see cref="MediaOptions"/> if found; otherwise, returns a 404 Not Found or 400 Bad Request.</returns>
    [HttpGet("media")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MediaOptions))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Media()
    {
        MediaOptions? option = await mediatr.Send(new GetMediaSettingsQuery());
        return option == null ? NotFound() : Ok(option);
    }

    /// <summary>
    /// Updates or creates the media settings.
    /// </summary>
    /// <param name="mediaOptions">The <see cref="MediaOptions"/> object to be saved or updated.</param>
    /// <returns>Returns the status of the operation or an error response.</returns>
    [HttpPost("media")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Media([FromBody] MediaOptions mediaOptions)
    {
        if (!ModelState.IsValid)
        {
            return ModelStateError(ModelState);
        }

        ReturnResult<int> result = await mediatr.Send(new CreateOrUpdateOptionCommand(new AppOptionsBase
        {
            AutoLoad = true,
            Key = "MediaOptions",
            Value = JsonSerializer.Serialize(mediaOptions)
        }));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Result);
    }

}
