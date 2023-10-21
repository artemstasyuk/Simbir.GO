using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Simbir.GO.Server.API.Controllers.Admin;

[ApiController]
[Route("api/v{version:apiVersion}/admin/transport")]
[Authorize(Roles = "Admin")]
public class AdminTransportController: ControllerBase
{
    /*GET /api/Admin/Transport
        описание: Получение списка всех транспортных средств
        параметры:
    start: int //Начало выборки
        count: int //Размер выборки
        transportType: “string” //Тип транспорта [Car, Bike, Scooter, All]
    ограничения: Только администраторы
    GET /api/Admin/Transport/{id}
    описание: Получение информации о транспортном средстве по id
        ограничения: Только администраторы
    POST /api/Admin/Transport
        описание: Создание нового транспортного средства
    body:
    {
        "ownerId": long, //id аккаунта владельца
        "canBeRented": bool, //Можно ли арендовать транспорт?
        "transportType": "string", //Тип транспорта [Car, Bike, Scooter]
        "model": "string", //Модель транспорта
        "color": "string", //Цвет транспорта
        "identifier": "string", //Номерной знак
        "description": "string", //Описание (может быть null)
        "latitude": double, //Географическая широта местонахождения транспорта
        "longitude": double, //Географическая долгота местонахождения транспорта
        "minutePrice": double, //Цена аренды за минуту (может быть null)
        "dayPrice": double //Цена аренды за сутки (может быть null)
    }
    ограничения: Только администраторы
    PUT /api/Admin/Transport/{id}
    описание: Изменение транспортного средства по id
        body:
    {
        "ownerId": long, //id аккаунта владельца
        "canBeRented": bool, //Можно ли арендовать транспорт?
        "transportType": "string", //Тип транспорта [Car, Bike, Scooter]
        "model": "string", //Модель транспорта
        "color": "string", //Цвет транспорта
        "identifier": "string", //Номерной знак
        "description": "string", //Описание (может быть null)
        "latitude": double, //Географическая широта местонахождения транспорта
        "longitude": double, //Географическая долгота местонахождения транспорта
        "minutePrice": double, //Цена аренды за минуту (может быть null)
        "dayPrice": double //Цена аренды за сутки (может быть null)
    }
    ограничения: Только администраторы
    DELETE /api/Admin/Transport/{id}
    описание: Удаление транспорта по id
    ограничения: Только администраторы*/
}