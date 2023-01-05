using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : BaseController
{
    public UserController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

    [HttpGet()]
    public IActionResult Get() => Ok($"User: {_user.Identity!.Name}");
}