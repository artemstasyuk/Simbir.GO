using Microsoft.AspNetCore.Mvc;

namespace Simbir.GO.Server.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/rent")]
public class RentController : ControllerBase
{
    
    //Todo search model
    /*lat: double //Географическая широта центра круга поиска транспорта
    long: double //Географическая долгота центра круга поиска транспорта
        radius: double //Радиус круга поиска транспорта
        type: “string” //Тип транспорта [Car, Bike, Scooter, All]*/
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return null;
    }
    
    [HttpGet("{rentId:guid}")]
    //ограничения: Только арендатор и владелец транспорта
    public async Task<IActionResult> Get([FromRoute] Guid rentId)
    {
        return null;
    }
    
    [HttpGet]
    //GET /api/Rent/MyHistory
        /*описание: Получение истории аренд текущего аккаунта
        ограничения: Только авторизованные пользователи*/
    public async Task<IActionResult> MyHistory()
    {
        return null;
    }
    
        
    
        
        /*GET /api/Rent/TransportHistory/{transportId}
    описание: Получение истории аренд транспорта
    ограничения: Только владелец этого транспорта
    POST /api/Rent/New/{transportId}
    описание: Аренда транспорта в личное пользование
        параметры:
    rentType: “string” //Тип аренды [Minutes, Days]
    ограничения: Только авторизованные пользователи, нельзя брать в аренду
    собственный транспорт
    POST /api/Rent/End/{rentId}
    описание: Завершение аренды транспорта по id аренды
    параметры:
    lat: double //Географическая широта местонахождения транспорта
    long: double //Географическая долгота местонахождения транспорта
        ограничения: Только человек который создавал эту аренду#1#*/
}