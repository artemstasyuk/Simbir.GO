using Microsoft.AspNetCore.Mvc;

namespace Simbir.GO.Server.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/transport")]
public class TransportController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return null;
    }
    
    [HttpPost()]
    public async Task<IActionResult> Create()
    {
        return null;
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update()
    {
        return null;
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete()
    {
        return null;
    }
}