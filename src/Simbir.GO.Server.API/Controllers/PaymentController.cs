using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Server.ApplicationCore.Interfaces;

namespace Simbir.GO.Server.API.Controllers;


[ApiController]
[Route("api/Payment")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("Hesoyam/{accountId:long}")]
    [Authorize]
    public async Task<IActionResult> AddBalance(long accountId)
    {
        if (User.IsInRole("Admin"))
             await _paymentService.UpdateBalanceAsync(accountId);

        return Ok();
    }
}