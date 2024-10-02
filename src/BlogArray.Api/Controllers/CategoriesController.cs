using BlogArray.Application.Features.Terms.Commands;
using BlogArray.Application.Features.Terms.Queries;
using BlogArray.Domain.Constants;
using BlogArray.Domain.DTOs;
using BlogArray.Domain.Enums;
using BlogArray.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogArray.Api.Controllers;

/// <summary>
/// Categories Controller
/// </summary>
/// <param name="mediatr"></param>
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = RoleConstants.AdminEditorAuthor)]
public class CategoriesController(IMediator mediatr) : BaseController
{
    private const TermType termType = TermType.Category;

    /// <summary>
    /// Retrieves a paginated list of categories.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve (default is 1).</param>
    /// <param name="pageSize">The size of the page to retrieve (default is 10).</param>
    /// <param name="searchTerm">Optional search term for filtering categories.</param>
    /// <returns>A paginated list of categories or an error response if not found.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<TermInfo>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null)
    {
        PagedResult<TermInfo> pagedResult = await mediatr.Send(new GetTermsQuery(pageNumber, pageSize, termType, searchTerm));
        return Ok(pagedResult);
    }

    /// <summary>
    /// Retrieves a category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category to retrieve.</param>
    /// <returns>The category if found, or an error response if not found.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TermInfo))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Get(int id)
    {
        TermInfo? term = await mediatr.Send(new GetTermByIdQuery(id, termType));
        return term == null ? CategoryErrors.NotFound(id) : Ok(term);
    }

    /// <summary>
    /// Retrieves a category by its slug.
    /// </summary>
    /// <param name="slug">The slug of the category to retrieve.</param>
    /// <returns>The category if found, or an error response if not found.</returns>
    [HttpGet("slug/{slug}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TermInfo))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Get(string slug)
    {
        TermInfo? term = await mediatr.Send(new GetTermBySlugQuery(slug, termType));
        return term == null ? CategoryErrors.SlugNotFound(slug) : Ok(term);
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="model">The category information to create.</param>
    /// <returns>The ID of the newly created category or an error response.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Post([FromBody] TermInfoDescription model)
    {
        if (!ModelState.IsValid)
        {
            return ModelStateError(ModelState);
        }

        ReturnResult<int> result = await mediatr.Send(new CreateTermCommand(model, termType));
        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Result);
    }

    /// <summary>
    /// Updates an existing category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category to update.</param>
    /// <param name="model">The updated category information.</param>
    /// <returns>A success message or an error response if the update fails.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Put(int id, [FromBody] TermInfoDescription model)
    {
        if (!ModelState.IsValid)
        {
            return ModelStateError(ModelState);
        }

        ReturnResult<int> result = await mediatr.Send(new UpdateTermCommand(model, id, termType));
        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Message);
    }

    /// <summary>
    /// Deletes an existing category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category to delete.</param>
    /// <returns>A success response or an error response if deletion fails.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Delete(int id)
    {
        ReturnResult<int> result = await mediatr.Send(new DeleteTermCommand(id, termType));
        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Message);
    }

}
