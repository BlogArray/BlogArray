using Microsoft.AspNetCore.Mvc;

namespace BlogArray.Controllers;

public class AdminController : Controller
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("/admin")]
    //[Authorize]
    public Task<VirtualFileResult> Admin()
    {
        return Task.FromResult(File("~/admin/index.html", "text/html"));
    }
}
