using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceSuite.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login()
        => Ok(new { token = "fake-token" });

    [HttpPost("register")]
    [AllowAnonymous]
    public IActionResult Register()
        => Ok();
}
