﻿using BlogArray.Application.Users.Commands;
using BlogArray.Application.Users.Queries;
using BlogArray.Domain.DTOs;
using BlogArray.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogArray.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IMediator mediatr) : BaseController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid input data.");
        }

        // Authenticate user using the repository
        LoginResult result = await mediatr.Send(new AuthenticateCommand(loginRequest));

        // Return appropriate response based on the authentication result
        if (!result.Success)
        {
            if (result.Type == "Invalid")
            {
                return Unauthorized(new { message = result.Message });
            }

            // Handle other types of failures if needed
            return BadRequest(new { message = result.Message });
        }

        // If authentication is successful, return the generated token
        return Ok(result.TokenData);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("userinfo")]
    [Authorize]
    public async Task<IActionResult> Userinfo()
    {
        UserInfo? user = await mediatr.Send(new GetUserByIdQuery(LoggedInUserID));

        if (user == null)
        {
            return UserErrors.NotFound(LoggedInUserID);
        }

        return Ok(user);
    }

}
