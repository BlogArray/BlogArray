using BlogArray.Application.Users.Commands;
using BlogArray.Application.Users.Queries;
using BlogArray.Domain.DTOs;
using BlogArray.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogArray.Api.Controllers;

/// <summary>
/// Controller responsible for account-related actions such as login, retrieving user information, 
/// and managing user accounts.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AccountController(IMediator mediatr) : BaseController
{
    /// <summary>
    /// Authenticates a user based on login request credentials and generates a token if successful.
    /// </summary>
    /// <param name="loginRequest">The request containing login credentials (username, password).</param>
    /// <returns>Returns an HTTP 200 status with a JWT token if successful, or an appropriate error status if not.</returns>
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid input data.");
        }

        // Authenticate user by sending an AuthenticateCommand to the mediator
        LoginResult result = await mediatr.Send(new AuthenticateCommand(loginRequest));

        // Return response based on the authentication result
        if (!result.Success)
        {
            return result.Type == "Invalid"
                ? UserErrors.Invalid()
                : UserErrors.UserLocked();
        }

        // Return JWT token if authentication is successful
        return Ok(result.TokenData);
    }

    /// <summary>
    /// Retrieves the current logged-in user's information.
    /// </summary>
    /// <returns>Returns an HTTP 200 status with the user's information, or a 404 if the user is not found.</returns>
    [Authorize]
    [HttpGet("userinfo")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserInfo))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> Userinfo()
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
    public async Task<IActionResult> CreateUser([FromBody] CreateUser createUser)
    {
        // Send a CreateUserCommand to the mediator to create a new user
        ReturnResult<int> result = await mediatr.Send(new CreateUserCommand(createUser, LoggedInUserID));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Result);
    }

    /// <summary>
    /// Updates the current user's information.
    /// </summary>
    /// <param name="editUserInfo">The updated user information.</param>
    /// <returns>Returns an HTTP 200 status if the user information is updated successfully, or an error response if it fails.</returns>
    [Authorize]
    [HttpPost("userinfo")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> UpdateUserinfo([FromBody] EditUserInfo editUserInfo)
    {
        // Send an EditUserCommand to the mediator to update user info
        ReturnResult<int> result = await mediatr.Send(new EditUserCommand(editUserInfo, LoggedInUserID));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Message);
    }
}
