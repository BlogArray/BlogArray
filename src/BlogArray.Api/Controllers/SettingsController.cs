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
/// Option Controller
/// </summary>
/// <param name="mediatr"></param>
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = RoleConstants.Admin)]
public class SettingsController(IMediator mediatr) : BaseController
{
    [HttpGet("siteinfo")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppOptionsBase))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> SiteInfo()
    {
        SiteInfo? option = await mediatr.Send(new GetSiteInfoQuery());
        return option == null ? NotFound() : Ok(option);
    }

    [HttpPost("siteinfo")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppOptionsBase))]
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

    [HttpGet("email")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppOptionsBase))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Email()
    {
        SMTPOptions? option = await mediatr.Send(new GetEmailSettingsQuery());
        return option == null ? NotFound() : Ok(option);
    }

    [HttpPost("email")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppOptionsBase))]
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

    [HttpGet("content")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppOptionsBase))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Content()
    {
        ContentOptions? option = await mediatr.Send(new GetContentOptionsQuery());
        return option == null ? NotFound() : Ok(option);
    }

    [HttpPost("content")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppOptionsBase))]
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


    [HttpGet("media")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppOptionsBase))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Media()
    {
        MediaOptions? option = await mediatr.Send(new GetMediaSettingsQuery());
        return option == null ? NotFound() : Ok(option);
    }

    [HttpPost("media")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppOptionsBase))]
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
