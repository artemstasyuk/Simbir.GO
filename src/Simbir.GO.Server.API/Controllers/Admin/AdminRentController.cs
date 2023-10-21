using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Simbir.GO.Server.API.Controllers.Admin;

[ApiController]
[Route("api/v{version:apiVersion}/admin/rent")]
[Authorize(Roles = "Admin")]
public class AdminRentController: ControllerBase
{
    // AdminRentController
    //     GET /api/Admin/Rent/{rentId}
    // описание: Получение информации по аренде по id
    // ограничения: Только администраторы
    // GET /api/Admin/UserHistory/{userId}
    // описание: Получение истории аренд пользователя с id={userId}
    // ограничения: Только администраторы
    // GET /api/Admin/TransportHistory/{transportId}
    // описание: Получение истории аренд транспорта с id={transportId}
    // ограничения: Только администраторы
    // POST /api/Admin/Rent
    //     описание: Создание новой аренды
    //     body:
    // {
    //     "transportId": long, //id транспортного средства, которое взяли в аренду
    //     "userId": long, //id аккаунта который будет владеть транспортом на время
    //     аренды
    //     "timeStart": "string", //дата и время начала аренды в ISO 8601
    //     "timeEnd": "string", //дата и время окончания аренды в ISO 8601 (может быть
    //     null)
    //     "priceOfUnit": double, //цена единицы вермени аренды (цена за минуту или за
    //     день)
    //     "priceType": "string", //тип оплаты [Minutes, Days]
    //     "finalPrice": 0 //финальная стоимость аренды (может быть null)
    // }
    // ограничения: Только администраторы
    // POST /api/Admin/Rent/End/{rentId}
    // описание: Завершение аренды транспорта по id аренды
    // параметры:
    // lat: double //Географическая широта местонахождения транспорта
    // long: double //Географическая долгота местонахождения транспорта
    //     ограничения: Только администраторы
    // PUT /api/Admin/Rent/{id}
    // описание: Изменение записи об аренде по id
    // body:
    // {
    //     "transportId": long, //id транспортного средства, которое взяли в аренду
    //     "userId": long, //id аккаунта который будет владеть транспортом на время
    //     аренды
    //     "timeStart": "string", //дата и время начала аренды в ISO 8601
    //     "timeEnd": "string", //дата и время окончания аренды в ISO 8601 (может быть
    //     null)
    //     "priceOfUnit": double, //цена единицы вермени аренды (цена за минуту или за
    //     день)
    //     "priceType": "string", //тип оплаты [Minutes, Days]
    //     "finalPrice": 0 //финальная стоимость аренды (может быть null)
    // }
    // ограничения: Только администраторы
    // DELETE /api/Admin/Rent/{rentId}
    // описание: Удаление информации об аренде по id
    // ограничения: Только администраторы
}