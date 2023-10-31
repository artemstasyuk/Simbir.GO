using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Server.ApplicationCore.Contracts.Rents;
using Simbir.GO.Server.ApplicationCore.Contracts.Transports;
using Simbir.GO.Server.ApplicationCore.Interfaces;
using Simbir.GO.Server.ApplicationCore.Interfaces.Authentication;

namespace Simbir.GO.Server.API.Controllers;

[ApiController]
[Route("api/Rent")]
public class RentController : ControllerBase
{

    private readonly IRentService _rentService;
    private readonly ITransportService _transportService;
    private readonly IMapper _mapper;

    public RentController(IRentService rentService, ITransportService transportService, IAccountService accountService, IAuthenticationService authenticationService, IMapper mapper)
    {
        _rentService = rentService;
        _transportService = transportService;
        _mapper = mapper;
    }
    
    
    [HttpGet("Transport")]
    public async Task<IActionResult> GetByLocation([FromQuery] TransportSearch search)
    {
        var result = await _transportService.GetListByLocationAsync(search);
        return Ok(_mapper.Map<List<TransportResult>>(result));
    }
    
    [HttpGet("{rentId:long}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] long rentId)
    {
        var result = await _rentService.GetByIdAsync(rentId);
        return Ok(_mapper.Map<RentResult>(result));
    }
    
    [HttpGet("MyHistory")]
    [Authorize]
    public async Task<IActionResult> GetAccountRents()
    {
        var result = await _rentService.GetAccountRentsAsync();
        return Ok(_mapper.Map<List<RentResult>>(result));
    }
    
    [HttpGet("TransportHistory/{transportId:long}")]
    [Authorize]
    public async Task<IActionResult> GetTransportRents([FromRoute] long transportId)
    {
        var result = await _rentService.GetTransportRentsAsync(transportId);
        return Ok(_mapper.Map<List<RentResult>>(result));
    }
    
    [HttpPost("New/{transportId:long}")]
    [Authorize]
    public async Task<IActionResult> Start([FromRoute] long transportId, [FromQuery] StartRentParams @params)
    {
        var result = await _rentService.StartAsync(transportId, @params);
        return Ok(_mapper.Map<RentResult>(result));
    }
    
    [HttpPost("End/{rentId:long}")]
    [Authorize]
    public async Task<IActionResult> End([FromRoute] long rentId, [FromQuery] EndRentParams @params)
    {
        var result = await _rentService.EndAsync(rentId, @params);
        return Ok(_mapper.Map<RentResult>(result));
    }
}