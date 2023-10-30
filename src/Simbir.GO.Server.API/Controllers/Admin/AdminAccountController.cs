using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Server.ApplicationCore.Contracts.Admin.Accounts;
using Simbir.GO.Server.ApplicationCore.Interfaces.Admin;
using Simbir.GO.Server.ApplicationCore.Specifications.Accounts;

namespace Simbir.GO.Server.API.Controllers.Admin;

[ApiController]
[Route("api/Admin/Account")]
[Authorize(Roles = "Admin")]
public class AdminAccountController: ControllerBase
{
    private readonly IAdminAccountService _adminAccountService;

    public AdminAccountController(IAdminAccountService adminAccountService)
    {
        _adminAccountService = adminAccountService;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> Get ([FromQuery] AccountFilter filter)
    {
        filter ??= new AccountFilter();
        var accounts = await _adminAccountService.GetByFiltersAsync(filter);
        return Ok(accounts);
    }
    
    
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById ([FromRoute] long id)
    {
        var account = await _adminAccountService.GetByIdAsync(id);
        return Ok(account);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Create ([FromBody] AdminCreateAccountRequest request)
    {
        var account = await _adminAccountService.CreateAsync(request);
        return Ok(account);
    }
    
    
    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update ([FromRoute] long id, [FromBody] AdminUpdateAccountRequest request)
    {
        var account = await _adminAccountService.UpdateAsync(id, request);
        return Ok(account);
    }
    
    
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete ([FromRoute] long id)
    {
        await _adminAccountService.DeleteAsync(id);
        return Ok();
    }
    
}