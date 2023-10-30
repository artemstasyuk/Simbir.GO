using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Server.ApplicationCore.Contracts.Transports;
using Simbir.GO.Server.ApplicationCore.Interfaces;

namespace Simbir.GO.Server.API.Controllers;

[ApiController]
[Route("api/Transport")]
public class TransportController : ControllerBase
{

    private readonly ITransportService _transportService;

    
    public TransportController(ITransportService transportService)
    {
        _transportService = transportService;
    }
    

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById([FromRoute] long id)
    {
        var result = await _transportService.GetByIdAsync(id);
        return Ok(result);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateTransportRequest request)
    {
        var result = await _transportService.CreateAsync(request);

        return Ok(result);
    }
    
    [HttpPut("{id:long}")]
    [Authorize]
    public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateTransportRequest request)
    {
        var result = await _transportService.UpdateAsync(id, request);
        return Ok(result);
    }
    
    [HttpDelete("{id:long}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
         await _transportService.DeleteAsync(id);
         return Ok();
    }
}