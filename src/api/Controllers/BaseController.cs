using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public abstract class BaseController : ControllerBase
{
    protected readonly ClaimsPrincipal _user;
    public BaseController(IHttpContextAccessor httpContextAccessor) =>
        _user = httpContextAccessor.HttpContext!.User;
}