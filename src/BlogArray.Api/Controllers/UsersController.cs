using BlogArray.Application.Features.Users.Commands;
using BlogArray.Application.Features.Users.Queries;
using BlogArray.Domain.Constants;
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
    /// Retrieves a paginated list of all users with optional search functionality.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve (default is 1).</param>
    /// <param name="pageSize">The number of users per page (default is 10).</param>
    /// <param name="searchTerm">
    /// Optional search term to filter users by their display name, username, or email.
    /// </param>
    /// <returns>
    /// A paginated list of users, optionally filtered by the search term, wrapped in an <see cref="IActionResult"/>.
    /// </returns>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<BasicUserInfoRole>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null)
    {
        // Retrieve users by sending a GetUsersQuery to the mediator
        PagedResult<BasicUserInfoRole> users = await mediatr.Send(new GetUsersQuery(pageNumber, pageSize, searchTerm));

        return Ok(users);
    }

    /// <summary>
    /// Creates a new user account based on the provided user information.
    /// </summary>
    /// <param name="createUser">The user information required for creating a new account.</param>
    /// <returns>Returns an HTTP 200 status if the user is created successfully with id, or an error response if creation fails.</returns>
    [Authorize(Roles = RoleConstants.Admin)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUser createUser)
    {
        if (!ModelState.IsValid)
        {
            return ModelStateError(ModelState);
        }

        // Send a CreateUserCommand to the mediator to create a new user
        ReturnResult<int> result = await mediatr.Send(new CreateUserCommand(createUser, LoggedInUserID));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Result);
    }

    /// <summary>
    /// Updates an existing user's information.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="editUserInfo">An object containing the updated user information.</param>
    /// <returns>Returns an HTTP 200 status if the user information is updated successfully, or an error response if it fails.</returns>
    [Authorize(Roles = RoleConstants.Admin)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> UpdateUserInfoAsync(int id, [FromBody] EditUserInfo editUserInfo)
    {
        if (!ModelState.IsValid)
        {
            return ModelStateError(ModelState);
        }

        // Send an EditUserCommand to the mediator to update user info
        ReturnResult<int> result = await mediatr.Send(new EditUserCommand(editUserInfo, id, LoggedInUserID));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Message);
    }

    /// <summary>
    /// Retrieves the current logged-in user's information.
    /// </summary>
    /// <returns>Returns an HTTP 200 status with the user's information, or a 404 if the user is not found.</returns>
    [Authorize]
    [HttpGet("me")]
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
    /// Updates the profile of the currently logged-in user.
    /// </summary>
    /// <param name="userProfile">An object containing the updated profile information.</param>
    /// <returns>Returns an HTTP 200 status if the user information is updated successfully, or an error response if it fails.</returns>
    [Authorize]
    [HttpPost("me")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> UpdateUserProfileAsync([FromBody] UserProfile userProfile)
    {
        if (!ModelState.IsValid)
        {
            return ModelStateError(ModelState);
        }

        // Send an EditUserCommand to the mediator to update user info
        ReturnResult<int> result = await mediatr.Send(new UpdateUserProfileCommand(userProfile, LoggedInUserID));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Message);
    }

    /// <summary>
    /// Deletes a user by their ID and optionally attributes the user's data to another user.
    /// </summary>
    /// <param name="id">The ID of the user to be deleted.</param>
    /// <param name="canAttributeTo">
    /// Specifies whether the deleted user's data should be attributed to another user.
    /// If set to <c>true</c>, the data will be attributed to the user specified by <paramref name="attributeToUser"/>.
    /// </param>
    /// <param name="attributeToUser">The ID of the user to attribute the deleted user's data, if applicable.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> indicating the result of the operation:
    /// - <see cref="StatusCodes.Status200OK"/> if the deletion is successful.
    /// - <see cref="StatusCodes.Status404NotFound"/> if the user is not found.
    /// - <see cref="StatusCodes.Status400BadRequest"/> if the request is invalid.
    /// </returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Delete(int id, bool canAttributeTo, int attributeToUser)
    {
        ReturnResult<int> result = await mediatr.Send(new DeleteUserCommand(id, canAttributeTo, attributeToUser));
        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Message);
    }

}
