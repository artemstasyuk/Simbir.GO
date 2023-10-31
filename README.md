# Задания для олимпиады Volga IT.

## Содержание

- [Задание](#задание)
  - [Требования](#требования)
- [Технологии](#технологии)
  - [Данные](#данные)
  - [Как запустить](#запуск)
 
  
## Задание

Разработка сервиса по аренде автомобилей под названием Simbir.GO. [Sibmir GO](https://volga-it.org/wp-content/uploads/2023/10/Задание-на-полуфинал-по-дисциплине-Backend-разработка-WEB-API.pdf).


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
* [Mapster](https://github.com/MapsterMapper/Mapster)
* [ArdalisSpecification](https://specification.ardalis.com/)
* [Serilog](https://serilog.net/)
* [PostgreSQL](https://www.postgresql.org/)
  

## Данные 
Для тестирования миграцией будут созданы 2 пользователя и 10 автомобилей
1. Admin - admin : 1234
2. Cusomer - customer : 1234
3. Также создаюся 2 аренды на автомоблили под id 1 и 7


## Запуск
1. Поменяйте строку подключения к бд. конфиг находится в appsetting.json в проекте Simbir.GO.API
```json
 "ConnectionStrings": {
    "Postgres": "Host=localhost;Port=5432;Database=Simbir.GO;Username={your-name};Password={your-password}"
  },
```
2.  Запустите проект




