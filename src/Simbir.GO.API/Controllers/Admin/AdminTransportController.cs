using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Server.ApplicationCore.Contracts.Admin.Transports;
using Simbir.GO.Server.ApplicationCore.Contracts.Transports;
using Simbir.GO.Server.ApplicationCore.Interfaces.Admin;
using Simbir.GO.Server.ApplicationCore.Specifications.Transports;

namespace Simbir.GO.API.Controllers.Admin;

[ApiController]
[Route("api/Admin/Transport")]
[Authorize(Roles = "Admin")]
public class AdminTransportController: ControllerBase
{
    private readonly IAdminTransportService _transportService;
    private readonly IMapper _mapper;

    public AdminTransportController(IAdminTransportService transportService, IMapper mapper)
    {
        _transportService = transportService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get ([FromQuery] TransportFilter filter)
    {
        filter ??= new TransportFilter();
        var result = await _transportService.GetByFiltersAsync(filter);
        return Ok(_mapper.Map<List<TransportResult>>(result));
    }
    
    
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById (long id)
    {
        var result = await _transportService.GetByIdAsync(id);
        return Ok(_mapper.Map<TransportResult>(result));
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Create ([FromBody] AdminCreateTransportRequest request)
    {
        var result = await _transportService.CreateAsync(request);
        return Ok(_mapper.Map<TransportResult>(result));
    }
    
    
    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update (long id, [FromBody] AdminUpdateTransportRequest request)
    {
        var result = await _transportService.UpdateAsync(id, request);
        return Ok(_mapper.Map<TransportResult>(result));
    }
    
    
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete (long id)
    {
        await _transportService.DeleteAsync(id);
        return Ok();
    }
}