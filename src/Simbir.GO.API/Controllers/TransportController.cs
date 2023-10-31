using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Server.ApplicationCore.Contracts.Transports;
using Simbir.GO.Server.ApplicationCore.Interfaces;

namespace Simbir.GO.API.Controllers;

[ApiController]
[Route("api/Transport")]
public class TransportController : ControllerBase
{

    private readonly ITransportService _transportService;
    private readonly IMapper _mapper;

    
    public TransportController(ITransportService transportService, IMapper mapper)
    {
        _transportService = transportService;
        _mapper = mapper;
    }
    

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _transportService.GetByIdAsync(id);
        return Ok(_mapper.Map<TransportResult>(result));
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateTransportRequest request)
    {
        var result = await _transportService.CreateAsync(request);
        return Ok(_mapper.Map<TransportResult>(result));
    }
    
    [HttpPut("{id:long}")]
    [Authorize]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateTransportRequest request)
    {
        var result = await _transportService.UpdateAsync(id, request);
        return Ok(_mapper.Map<TransportResult>(result));
    }
    
    [HttpDelete("{id:long}")]
    [Authorize]
    public async Task<IActionResult> Delete(long id)
    {
         await _transportService.DeleteAsync(id);
         return Ok();
    }
}