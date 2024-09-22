using BlogArray.Application.Features.Users.Commands;
using BlogArray.Application.Features.Users.Queries;
using BlogArray.Domain.DTOs;
using BlogArray.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogArray.Api.Controllers;

/// <summary>
/// Controller responsible for user-related actions such as managing user accounts.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UsersController(IMediator mediatr) : BaseController
{
    /// <summary>
    /// Retrieves the current logged-in user's information.
    /// </summary>
    /// <returns>Returns an HTTP 200 status with the user's information, or a 404 if the user is not found.</returns>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserInfo))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> UserInfoAsync()
    {
        // Retrieve user info by sending a GetUserByIdQuery to the mediator
        UserInfo? user = await mediatr.Send(new GetUserByIdQuery(LoggedInUserID));

        return user == null ? UserErrors.NotFound(LoggedInUserID) : Ok(user);
    }

    /// <summary>
    /// Creates a new user account based on the provided user information.
    /// </summary>
    /// <param name="createUser">The user information required for creating a new account.</param>
    /// <returns>Returns an HTTP 200 status if the user is created successfully with id, or an error response if creation fails.</returns>
    [Authorize]
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUser createUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid input data.");
        }

        // Send a CreateUserCommand to the mediator to create a new user
        ReturnResult<int> result = await mediatr.Send(new CreateUserCommand(createUser, LoggedInUserID));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Result);
    }

    /// <summary>
    /// Updates the user information bassed on id.
    /// </summary>
    /// <param name="editUserInfo">The updated user information.</param>
    /// <param name="id">The ID of the user whose information is to be updated.</param>
    /// <returns>Returns an HTTP 200 status if the user information is updated successfully, or an error response if it fails.</returns>
    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> UpdateUserInfoAsync([FromBody] EditUserInfo editUserInfo, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid input data.");
        }

        // Send an EditUserCommand to the mediator to update user info
        ReturnResult<int> result = await mediatr.Send(new EditUserCommand(editUserInfo, id, LoggedInUserID));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Message);
    }

    /// <summary>
    /// Updates the current user's information.
    /// </summary>
    /// <param name="editUserInfo">The updated user information.</param>
    /// <returns>Returns an HTTP 200 status if the user information is updated successfully, or an error response if it fails.</returns>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> UpdateUserProfileAsync([FromBody] EditUserInfo editUserInfo)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid input data.");
        }

        // Send an EditUserCommand to the mediator to update user info
        ReturnResult<int> result = await mediatr.Send(new EditUserCommand(editUserInfo, LoggedInUserID, LoggedInUserID));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Message);
    }
}
