using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Server.ApplicationCore.Contracts.Admin.Rents;
using Simbir.GO.Server.ApplicationCore.Interfaces.Admin;

namespace Simbir.GO.Server.API.Controllers.Admin;

[ApiController]
[Route("api/Admin/Rent")]
[Authorize(Roles = "Admin")]
public class AdminRentController : ControllerBase
{
    private readonly IAdminRentService _rentService;

    public AdminRentController(IAdminRentService rentService)
    {
        _rentService = rentService;
    }


    [HttpGet("{rentId:long}")]
    public async Task<IActionResult> GetById([FromRoute] long rentId)
    {
        var result = await _rentService.GetByIdAsync(rentId);
        return Ok(result);
    }

    [HttpGet("UserHistory/{userId:long}")]
    public async Task<IActionResult> GetUserRents([FromRoute] long userId)
    {
        var result = await _rentService.GetAccountRentsAsync(userId);
        return Ok(result);
    }


    [HttpGet("TransportHistory/{transportId:long}")]
    public async Task<IActionResult> GetTransportRents([FromRoute] long transportId)
    {
        var result = await _rentService.GetTransportRentsAsync(transportId);
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AdminCreateRentRequest request)
    {
        var transport = await _rentService.CreateAsync(request);
        return Ok(transport);
    }


    [HttpPut("{id:long}")]
    public async Task<IActionResult> Create([FromQuery] long id, [FromBody] AdminUpdateRentRequest request)
    {
        var transport = await _rentService.UpdateAsync(id, request);
        return Ok(transport);
    }


    [HttpPost("End/{rentId:long}")]
    [Authorize]
    public async Task<IActionResult> End([FromRoute] long rentId, [FromQuery] AdminEndRentRequest request)
    {
        var result = await _rentService.EndAsync(rentId, request);
        return Ok(result);
    }


    [HttpDelete("{rentId:long}")]
    public async Task<IActionResult> Delete([FromRoute] long rentId)
    {
        await _rentService.DeleteAsync(rentId);
        return Ok();
    }
}