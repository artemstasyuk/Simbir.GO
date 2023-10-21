using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Server.Contracts.Auth;

namespace Simbir.GO.Server.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/account")]
[AllowAnonymous]
public class AccountController: ControllerBase
{
    [HttpPost("sign-out")]
    public async Task<IActionResult> Register([FromForm] RegisterRequest request)
    {
        return null;
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> Login([FromForm] LoginRequest request)
    {
        return null;
    }

    [HttpPost("sign-out")]
    public async Task<IActionResult> SignOut()
    {
        return null;
    }


    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        return null;
    }
    
    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> Update()
    {
        return null;
    }
}
    
    