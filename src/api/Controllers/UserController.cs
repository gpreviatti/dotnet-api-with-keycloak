using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class UserController : BaseController
{
    public UserController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

    [HttpGet()]
    public IActionResult Get() => Ok($"User: {_user.Identity!.Name}");
}