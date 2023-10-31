using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Server.ApplicationCore.Contracts.Admin.Rents;
using Simbir.GO.Server.ApplicationCore.Contracts.Rents;
using Simbir.GO.Server.ApplicationCore.Interfaces.Admin;

namespace Simbir.GO.Server.API.Controllers.Admin;

[ApiController]
[Route("api/Admin/Rent")]
[Authorize(Roles = "Admin")]
public class AdminRentController : ControllerBase
{
    private readonly IAdminRentService _rentService;
    private readonly IMapper _mapper;

    public AdminRentController(IAdminRentService rentService, IMapper mapper)
    {
        _rentService = rentService;
        _mapper = mapper;
    }


    [HttpGet("{rentId:long}")]
    public async Task<IActionResult> GetById(long rentId)
    {
        var result = await _rentService.GetByIdAsync(rentId);
        return Ok(_mapper.Map<RentResult>(result));
    }

    [HttpGet("UserHistory/{userId:long}")]
    public async Task<IActionResult> GetUserRents(long userId)
    {
        var result = await _rentService.GetAccountRentsAsync(userId);
        return Ok(_mapper.Map<List<RentResult>>(result));
    }


    [HttpGet("TransportHistory/{transportId:long}")]
    public async Task<IActionResult> GetTransportRents(long transportId)
    {
        var result = await _rentService.GetTransportRentsAsync(transportId);
        return Ok(_mapper.Map<List<RentResult>>(result));
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AdminCreateRentRequest request)
    {
        var result = await _rentService.CreateAsync(request);
        return Ok(_mapper.Map<RentResult>(result));
    }


    [HttpPut("{id:long}")]
    public async Task<IActionResult> Create(long id, [FromBody] AdminUpdateRentRequest request)
    {
        var result = await _rentService.UpdateAsync(id, request);
        return Ok(_mapper.Map<RentResult>(result));
    }


    [HttpPost("End/{rentId:long}")]
    public async Task<IActionResult> End(long rentId, [FromQuery] AdminEndRentRequest request)
    {
        var result = await _rentService.EndAsync(rentId, request);
        return Ok(_mapper.Map<RentResult>(result));
    }


    [HttpDelete("{rentId:long}")]
    public async Task<IActionResult> Delete(long rentId)
    {
        await _rentService.DeleteAsync(rentId);
        return Ok();
    }
}