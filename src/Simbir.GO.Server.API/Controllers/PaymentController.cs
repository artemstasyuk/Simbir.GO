using Microsoft.AspNetCore.Mvc;

namespace Simbir.GO.Server.API.Controllers;


[ApiController]
[Route("api/v{version:apiVersion}/checkout")]
public class PaymentController : ControllerBase
{
    /*POST /api/Payment/Hesoyam/{accountId}
    описание: Добавляет на баланс аккаунта с id={accountId} 250 000 денежных единиц.
        ограничения: Администратор может добавить баланс всем, обычный пользователь
    только себе*/
}