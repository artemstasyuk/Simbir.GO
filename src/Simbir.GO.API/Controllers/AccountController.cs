using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Server.ApplicationCore.Contracts.Accounts;
using Simbir.GO.Server.ApplicationCore.Contracts.Authentication;
using Simbir.GO.Server.ApplicationCore.Interfaces;
using Simbir.GO.Server.ApplicationCore.Interfaces.Authentication;


namespace Simbir.GO.API.Controllers;

[ApiController]
[Route("api/Account")]
public class AccountController: ControllerBase
{

    private readonly IAccountService _accountService;
    private readonly IAuthenticationService _authenticationService;
    private readonly IMapper _mapper;

    public AccountController(IAccountService accountService, IAuthenticationService authenticationService, IMapper mapper)
    {
        _accountService = accountService;
        _authenticationService = authenticationService;
        _mapper = mapper;
    }

    [HttpGet("Me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var result = await _authenticationService.GetCurrentUserAsync();
        return Ok(_mapper.Map<AccountResult>(result));
    }
    
    [HttpPost("SignIn")]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {
        var result = await _authenticationService.SignInAsync(request);
        return Ok(result);
    }
    
    [HttpPost("SignUp")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        var result = await _authenticationService.SignUpAsync(request);
        return Ok(result);
    }
    
    
    [HttpPost("SignOut")]
    [Authorize]
    public new async Task<IActionResult> SignOut()
    {
        await _authenticationService.SignOutAsync();
        return Ok();
    }
   
    
    [HttpPut("Update")]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] EditAccountRequest request)
    {
        var result = await _accountService.EditAsync(request);
        return Ok(_mapper.Map<AccountResult>(result));
    }
    
    

    
    
    
}
    
    