using BlogArray.Application.Features.AppOptions.Commands;
using BlogArray.Application.Features.AppOptions.Queries;
using BlogArray.Domain.Constants;
using BlogArray.Domain.DTOs;
using BlogArray.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogArray.Api.Controllers;

/// <summary>
/// Option Controller
/// </summary>
/// <param name="mediatr"></param>
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = RoleConstants.Admin)]
public class OptionsController(IMediator mediatr) : BaseController
{
    /// <summary>
    /// Retrieves a paginated list of options.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve (default is 1).</param>
    /// <param name="pageSize">The size of the page to retrieve (default is 10).</param>
    /// <param name="onlyAutoload">Set true if you need only auto loaded options.</param>
    /// <returns>A paginated list of options or an error response if not found.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<AppOptionsBase>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> GetAllAsync(int pageNumber, int pageSize, bool onlyAutoload)
    {
        PagedResult<AppOptionsBase> pagedResult = await mediatr.Send(new GetOptionsQuery(pageNumber, pageSize, onlyAutoload));
        return Ok(pagedResult);
    }

    /// <summary>
    /// Retrieves a option by its slug.
    /// </summary>
    /// <param name="key">The slug of the option to retrieve.</param>
    /// <returns>The option if found, or an error response if not found.</returns>
    [HttpGet("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppOptionsBase))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Get(string key)
    {
        AppOptionsBase? option = await mediatr.Send(new GetOptionByKeyQuery(key));
        return option == null ? OptionErrors.NotFound(key) : Ok(option);
    }

    /// <summary>
    /// Creates or updates a new option.
    /// </summary>
    /// <param name="model">The option information to create.</param>
    /// <returns>The ID of the newly created option or an error response.</returns>
    [HttpPost("createorupdate")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Post([FromBody] AppOptionsBase model)
    {
        if (!ModelState.IsValid)
        {
            return ModelStateError(ModelState);
        }

        ReturnResult<int> result = await mediatr.Send(new CreateOrUpdateOptionCommand(model));
        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Result);
    }

    /// <summary>
    /// Deletes an existing option by its key.
    /// </summary>
    /// <param name="key">The key of the option to delete.</param>
    /// <returns>A success response or an error response if deletion fails.</returns>
    [HttpDelete("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Delete(string key)
    {
        ReturnResult<int> result = await mediatr.Send(new DeleteOptionCommand(key));
        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Message);
    }

}

