using BlogArray.Domain.DTOs;
using BlogArray.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;

namespace BlogArray.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected int LoggedInUserID => Converter.ToInt(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

    protected int TimezoneOffset => Converter.ToInt(User.Claims.FirstOrDefault(c => c.Type == "TimezoneOffset")?.Value);

    protected string LoggedInUserEmail => Converter.ToString(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value);

    protected string UserAgent => Request.Headers[HeaderNames.UserAgent];


    protected IActionResult JsonError(dynamic message)
    {
        ReturnResult returnResult = new()
        {
            Status = false,
            Message = Converter.ToString(message)
        };
        return BadRequest(returnResult);
    }

    protected IActionResult ModelStateError(ModelStateDictionary ModelState)
    {
        IEnumerable<string> errors = ModelState.Values.SelectMany(state => state.Errors).Select(error => error.ErrorMessage);
        ReturnResult returnResult = new()
        {
            Status = false,
            Message = string.Join(". ", errors)
        };
        return BadRequest(returnResult);
    }

    protected IActionResult JsonSuccess(dynamic message)
    {
        ReturnResult returnResult = new()
        {
            Status = true,
            Message = Converter.ToString(message)
        };

        return Ok(returnResult);
    }

    protected IActionResult JsonSuccess<T>(dynamic message, T result)
    {
        ReturnResult<T> returnResult = new()
        {
            Status = true,
            Message = Converter.ToString(message),
            Result = result
        };

        return Ok(returnResult);
    }
}
