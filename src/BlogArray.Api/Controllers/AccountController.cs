using BlogArray.Application.Features.Account.Commands;
using BlogArray.Domain.DTOs;
using BlogArray.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogArray.Api.Controllers;

/// <summary>
/// Controller responsible for account-related actions such as login, register new user, 
/// and managing identity.
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
            return ModelStateError(ModelState);
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
    /// Creates a new user account based on the provided user information.
    /// </summary>
    /// <param name="registerRequest">The user information required for creating a new account.</param>
    /// <returns>Returns an HTTP 200 status if the user is created successfully with id, or an error response if creation fails.</returns>
    [Authorize]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest registerRequest)
    {
        if (!ModelState.IsValid)
        {
            return ModelStateError(ModelState);
        }

        ReturnResult<int> result = await mediatr.Send(new RegisterUserCommand(registerRequest));

        return !result.Status ? ErrorDetails.CreateResponse(result.Code, result.Title, result.Message) : Ok(result.Result);
    }

}
