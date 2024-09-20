using BlogArray.Domain.DTOs;
using BlogArray.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogArray.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AccountController(IAccountRepository accountRepository) : BaseController
{
    // POST: api/account/authenticate
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] SignIn signIn)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid input data.");
        }

        // Authenticate user using the repository
        Domain.DTOs.SignInResult result = await accountRepository.Authenticate(signIn);

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
}
