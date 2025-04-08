# AdvertisingPlatformsApp

Сервис для управления рекламными площадками и их локациями. Позволяет:
- Загружать данные о площадках из файла
- Искать подходящие площадки для указанной локации

Соответствует [требованиям тестового задания](https://docs.google.com/document/d/13q8lwvRXF9JxK3EbBIZig9ddqN-p-WGeq0c3_ahLW64/edit).

## Структура проекта

```
AdvertisingPlatformsApp/
├── Api / - Веб-интерфейс (ASP.NET Core)
├── Core/ - Бизнес-логика и хранилище
└── Tests/ - Юнит тесты
```

## Запуск проекта

### Требования

- .NET 8 SDK
- IDE (Visual Studio/Rider/VS Code)

### Установка
```bash
git clone https://github.com/llllenivka/AdvertisingPlatformsApp.git
cd AdvertisingPlatformsApp/AdvertisingPlatforms/Api
dotnet restore
```

### Запуск

```bash
dotnet run
```

- Сервис будет доступен на: http://localhost:5245

- Для упрощения тестирования можно использовать
Swagger: http://localhost:5245/swagger/index.html

- Пример файлов для тестирования можно найти в корне проекта :
    1. test.txt
    2. testErrorData.txt

## API Документация
### Загрузка рекламных площадок

Endpoint: `POST /api/adplatforms/upload`

Параметры:

file: Текстовый файл в формате:

```
Площадка1:/локация1,/локация2
Площадка2:/локация3
```

Пример запроса:

```bash
curl -X POST -F "file=@test.txt" http://localhost:5245/api/AdvertisingPlatforms
```
Ответ:

```
All: 51
Correct: 51
Incorrect: 0
```

### Поиск площадок по локации

Endpoint: `GET /api/adplatforms/search?location={location}`

Параметры:

location: локация (например /ru/msk)

Пример запроса:

```bash
curl "http://localhost:5245/api/AdvertisingPlatforms/locationStorage/%2Fru"
```

Ответ:

```
["Глобальная реклама","Яндекс.Директ","Google Ads"]
```

## Тестирование

Перейти в директорию Tests

Запуск тестов:
```bash
dotnet test
```
