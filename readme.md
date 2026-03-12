# GazpromTestProject
Решение тестового задания с использованем .NET, PostgreSQL и Docker
## Архитектура
### Проект разделён на слои:
1. Domain — сущности и базовый Entity
2. Application — DTO, сервисы, интерфейсы репозиториев, валидация (FluentValidation)
3. Infrastructure — EF Core, репозитории, конфигурации сущностей, миграции
4. Web — API контроллеры
## Реализованные требования:
1. ### Справочник “Поставщик”
   1. Поля: 
      1. Id - int, внутренний ключ
      2. Name - string
      3. CreatedAt - DateTime  
        Заполнен 15 тестовыми значениями
2. ### Справочник "Предложения"
   1. Поля:
      1. Id - int, внутренний ключ
      2. Brand - string
      3. Model - string
      4. Supplier - поле для упрощения построения дальнейшей логики приложения
      5. SupplierId - int, внешний ключ
      6. CreatedAt - DateTime  
Заполнен 5 тестовыми значениями
## Основные эндпоинты разработанного API
1. POST /api/offers/create — создание оффера
2. GET /api/offers/search — поиск по Brand, Model, SupplierName
3. GET /api/suppliers/top — топ поставщиков по количеству офферов
## Что сделано
1. Реализованы сущности Offer, Supplier и базовый класс Entity
2. Вынесены конфигурации основных таблиц базы данных в Infrastructure (IEntityTypeConfiguration<T>)
3. Настроена база данных с помощью DbContext. Также реализован метод для первоначального заполнения указанных таблиц
4. Реализованы репозитории и сервисы Application слоя
5. Реализована валидация запросов с помощью FluentValidation
7. Program.cs упрощён, конфигурации вынесены в AppStartup
8. Docker Compose для PostgreSQL
## Запуск
   1. Docker: docker compose up --build
   2. dotnet run --project src/Web/Web.csproj
## Возможные улучшения
1. Добавить авторизацию и аутентификацию пользователей
2. Перейти от исключений для обработки ошибок к Result паттерну
3. Добавить Health Checks (/health) и проверку доступности БД
5. Убрать строку подключения к бд из appsettings.json и использовать для этого vault файлы
6. Добавить Serilog + request logging
7. Добавить unit‑тесты и интеграционные тесты