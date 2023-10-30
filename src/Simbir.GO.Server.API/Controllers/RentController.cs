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
    private readonly IAccountService _accountService;
    private readonly IAuthenticationService _authenticationService;

    public RentController(IRentService rentService, ITransportService transportService, IAccountService accountService, IAuthenticationService authenticationService)
    {
        _rentService = rentService;
        _transportService = transportService;
        _accountService = accountService;
        _authenticationService = authenticationService;
    }
    
    
    
    [HttpGet("Transport")]
    public async Task<IActionResult> GetByLocation([FromQuery] TransportSearch search)
    {
        var result = await _transportService.GetListByLocationAsync(search);
        return Ok(result);
    }
    
    [HttpGet("{rentId:long}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] long rentId)
    {
        var result = await _rentService.GetByIdAsync(rentId);
        return Ok(result);
    }
    
    [HttpGet("MyHistory")]
    [Authorize]
    public async Task<IActionResult> GetAccountRents()
    {
        var result = await _rentService.GetAccountRentsAsync();
        return Ok(result);
    }
    
    [HttpGet("TransportHistory/{transportId:long}")]
    [Authorize]
    public async Task<IActionResult> GetTransportRents([FromForm] long transportId)
    {
        var result = await _rentService.GetTransportRentsAsync(transportId);
        return Ok(result);
    }
    
    [HttpPost("New/{transportId:long}")]
    [Authorize]
    public async Task<IActionResult> Start([FromRoute] long transportId, [FromQuery] StartRentRequest request)
    {
        var result = await _rentService.StartAsync(transportId, request);
        return Ok(result);
    }
    
    [HttpPost("End/{rentId:long}")]
    [Authorize]
    public async Task<IActionResult> End([FromRoute] long rentId, [FromQuery] EndRentRequest request)
    {
        var result = await _rentService.EndAsync(rentId, request);
        return Ok(result);
    }
}