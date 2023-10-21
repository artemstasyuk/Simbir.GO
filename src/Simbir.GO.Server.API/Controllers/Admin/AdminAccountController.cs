using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Server.Domain.Enums;

namespace Simbir.GO.Server.API.Controllers.Admin;

[ApiController]
[Route("api/v{version:apiVersion}/admin/account")]
[Authorize(Roles = "Admin")]
public class AdminAccountController: ControllerBase
{
    /*GET /api/Admin/Account
        описание: Получение списка всех аккаунтов
    параметры:
    start: int //Начало выборки
        count: int //Размер выборки
        ограничения: Только администраторы
    GET /api/Admin/Account/{id}
    описание: Получение информации об аккаунте по id
    ограничения: Только администраторы
    POST /api/Admin/Account
        описание: Создание администратором нового аккаунта
    body:
    {
        "username": "string", //имя пользователя
        "password": "string", //пароль
        "isAdmin": bool, //является ли пользователь администратором
        "balance": double //баланс пользователя
    }
    ограничения: Только администраторы, нельзя создать аккаунт с уже существующим в
    системе username
    PUT /api/Admin/Account/{id}
    описание: Изменение администратором аккаунта по id
        body:
    {
        "username": "string", //имя пользователя
        "password": "string", //пароль
        "isAdmin": bool, //является ли пользователь администратором
        "balance": double //баланс пользователя
    }
    ограничения: Только администраторы, нельзя изменять username на уже
    существующий в системе
        DELETE /api/Admin/Account/{id}
    описание: Удаление аккаунта по id
    ограничения: Только администраторы*/
}