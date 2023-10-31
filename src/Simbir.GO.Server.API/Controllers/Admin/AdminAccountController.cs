using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Server.ApplicationCore.Contracts.Accounts;
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
    private readonly IMapper _mapper;

    public AdminAccountController(IAdminAccountService adminAccountService, IMapper mapper)
    {
        _adminAccountService = adminAccountService;
        _mapper = mapper;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> Get ([FromQuery] AccountFilter filter)
    {
        filter ??= new AccountFilter();
        var result = await _adminAccountService.GetByFiltersAsync(filter);
        return Ok(_mapper.Map<List<AccountResult>>(result));
    }
    
    
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById ([FromRoute] long id)
    {
        var result = await _adminAccountService.GetByIdAsync(id);
        return Ok(_mapper.Map<AccountResult>(result));
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Create ([FromBody] AdminCreateAccountRequest request)
    {
        var result = await _adminAccountService.CreateAsync(request);
        return Ok(_mapper.Map<AccountResult>(result));
    }
    
    
    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update ([FromRoute] long id, [FromBody] AdminUpdateAccountRequest request)
    {
        var result = await _adminAccountService.UpdateAsync(id, request);
        return Ok(_mapper.Map<AccountResult>(result));
    }
    
    
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete ([FromRoute] long id)
    {
        await _adminAccountService.DeleteAsync(id);
        return Ok();
    }
    
}