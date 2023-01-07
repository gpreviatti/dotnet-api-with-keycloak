using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Authorize("IsAdmin")]
[Route("[controller]")]
public class AdminController : BaseController
{
    public AdminController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

    [HttpGet]
    public IActionResult Get() => Ok($"User: {_user.Identity!.Name}");
}