# Тестовое задание АЭБ
 
## Разработка API для веб-приложения «Список задач» на .NET Core/C#

### Используемые инструменты:

- Visual Studio 2022
- .NET 6
- Шаблон проектирования ASP.NET Core Web API
- EntityFrameworkCore
- Swagger
- FluentValidation
- PostgreSQL
- Docker Desktop
- React

### Шаги разработки:

1. ***Установка и настройка окружения***
2. ***Создание проекта***
3. ***Установка Entity Framework Core***
4. [***Создание моделей данных***](test_aeb/test_aeb/Models/ToDo_model.cs)(Id, Title, Description, Due_Time, Create_Time, Completion_Time, status)
5. [***Создание контекста***](test_aeb/test_aeb/Context/ToDo_Context.cs)(ToDoContext)
6. [***Создание контроллеров***](test_aeb/test_aeb/Controllers/ToDoController.cs)
7. [***Валидация входных данных***](test_aeb/test_aeb/Validators/TaskValidator.cs)
8. ***Мини-Документирование API***
9. ***Развертывание в Docker***
10. ***Добавление мини-фронтенда***