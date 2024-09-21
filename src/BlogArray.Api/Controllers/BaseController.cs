using BlogArray.Domain.DTOs;
using BlogArray.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace BlogArray.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected int LoggedInUserID => Converter.ToInt(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

    //protected int TimezoneOffset => Converter.ToInt(User.Claims.FirstOrDefault(c => c.Type == "TimezoneOffset")?.Value);

    protected string LoggedInUserEmail => Converter.ToString(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value);

    //protected string UserAgent => Request.Headers[HeaderNames.UserAgent];

    protected IActionResult ModelStateError(ModelStateDictionary ModelState)
    {
        string[] errors = ModelState.Values.SelectMany(state => state.Errors).Select(error => error.ErrorMessage).ToArray();

        return BadRequest(ErrorDetails.BadRequest("", "", errors));
    }

    protected IActionResult Success<T>(T result)
    {
        return Ok(result);
    }
}
