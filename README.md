# Задания для олемпиады Volga IT.

# Simbir.GO Transport rent web api

## Содержание

- [Задание](#задание)
  - [Требования](#требования)
- [Технологии](#технологии)
  - [Как запустить](#запуск)
 
  
## Задание

Разработка сервиса по аренде автомобилейпод названием “Simbir.GO”. [Sibmir GO](clck.ru/36J6AV).


## Требования
Разработка приложения
Необходимо реализовать данные контроллеры:
1. Контроллер аккаунтов / Админ контроллер аккаунтов
2. Контроллер оплаты
3. Контроллер транспорта / Админ контроллер транспорта
4. Контроллер аренды / Админ контроллер аренды


## Технологии 

* [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
* [Mapster](https://automapper.org/)
* [ArdalisSpecification](https://specification.ardalis.com/)
* [Serilog](https://automapper.org/)
* [PostgreSQL]([https://automapper.org/](https://serilog.net/))
  

#### Запуск
1. Поменяйте строку подключения к бд
```json
 "ConnectionStrings": {
    "Postgres": "Host=localhost;Port=5432;Database=Simbir.GO;Username={your-name};Password={your-password}"
  },
```
2.  Запустите проект




