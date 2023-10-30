using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Server.ApplicationCore.Contracts.Accounts;
using Simbir.GO.Server.ApplicationCore.Contracts.Authentication;
using Simbir.GO.Server.ApplicationCore.Interfaces;
using Simbir.GO.Server.ApplicationCore.Interfaces.Authentication;


namespace Simbir.GO.Server.API.Controllers;

[ApiController]
[Route("api/Account")]
public class AccountController: ControllerBase
{

    private readonly IAccountService _accountService;
    private readonly IAuthenticationService _authenticationService;

    public AccountController(IAccountService accountService, IAuthenticationService authenticationService)
    {
        _accountService = accountService;
        _authenticationService = authenticationService;
    }

    [HttpPost("SignUp")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromForm] SignUpRequest request)
    {
        var result = await _authenticationService.SignUpAsync(request);
        return Ok(result);
    }

    
    [HttpPost("SignIn")]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromForm] SignInRequest request)
    {
        var result = await _authenticationService.SignInAsync(request);
        return Ok(result);
    }
    
    
    [HttpPost("SignOut")]
    [Authorize]
    public async Task<IActionResult> SignOut()
    {
        //TODO implement distruction refresh token logic 
        await _authenticationService.SignOutAsync();
        return Ok();
    }
    
    
    [HttpGet("Me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var result = await _authenticationService.GetCurrentUserAsync();
        return Ok(result);
    }
    
    
    [HttpPut("Update")]
    [Authorize]
    public async Task<IActionResult> Update(EditAccountRequest request)
    {
        var result = await _accountService.EditAsync(request);
        return Ok(result);
    }
}
    
    