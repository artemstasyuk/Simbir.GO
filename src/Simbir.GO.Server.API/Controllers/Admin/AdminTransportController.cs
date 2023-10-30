using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Server.ApplicationCore.Contracts.Admin.Transports;
using Simbir.GO.Server.ApplicationCore.Interfaces.Admin;
using Simbir.GO.Server.ApplicationCore.Specifications.Transports;

namespace Simbir.GO.Server.API.Controllers.Admin;

[ApiController]
[Route("api/Admin/Transport")]
[Authorize(Roles = "Admin")]
public class AdminTransportController: ControllerBase
{
    private readonly IAdminTransportService _transportService;

    public AdminTransportController(IAdminTransportService transportService)
    {
        _transportService = transportService;
    }

    [HttpGet]
    public async Task<IActionResult> Get ([FromQuery] TransportFilter filter)
    {
        filter ??= new TransportFilter();
        var transports = await _transportService.GetByFiltersAsync(filter);
        return Ok(transports);
    }
    
    
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById ([FromRoute] long id)
    {
        var transport = await _transportService.GetByIdAsync(id);
        return Ok(transport);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Create ([FromBody] AdminCreateTransportRequest request)
    {
        var transport = await _transportService.CreateAsync(request);
        return Ok(transport);
    }
    
    
    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update ([FromRoute] long id, [FromBody] AdminUpdateTransportRequest request)
    {
        var transport = await _transportService.UpdateAsync(id, request);
        return Ok(transport);
    }
    
    
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete ([FromRoute] long id)
    {
        await _transportService.DeleteAsync(id);
        return Ok();
    }
}