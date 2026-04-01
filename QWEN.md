# ReporterApp - Контекст проекта

## Обзор проекта

**ReporterApp** — это WPF-приложение для работы с отчётами по автомобилям. Это первый серьёзный проект разработчика на WPF (до этого использовался только WinForms). Приложение находится в стадии **0.1 alpha**.

### Основное назначение
- Управление данными об автомобилях (номера, топливо, парковки, дополнительная информация)
- Генерация отчётов
- Работа с файлами данных
- Интеграция с веб-ресурсом (автоматический запуск при старте)

## Технологический стек

| Компонент | Технология |
|-----------|------------|
| Платформа | .NET 10.0 |
| UI Framework | WPF (Windows Presentation Foundation) |
| Язык | C# с nullable reference types |
| Тестирование | xUnit, Moq, AutoFixture, coverlet |
| Паттерны | MVVM (ObservableObject, data binding) |

## Структура решения

```
ReporterApp/
├── ReporterApp.Core/          # Ядро бизнес-логики
│   ├── Cars/
│   │   ├── Car.cs             # Модель автомобиля с INotifyPropertyChanged
│   │   ├── CarEnumerator.cs   # Перебор автомобилей
│   │   └── CarUtils.cs        # Утилиты для работы с автомобилями
│   └── Utils/
│       └── ObservableObject.cs   # Базовый класс для MVVM
│
├── ReporterApp.DAL/           # Data Access Layer
│   └── FileAccess/
│       ├── CarsFileReader.cs
│       ├── CarsFileWriter.cs
│       ├── CarsOperationInfo.cs
│       └── FileOperationResult.cs
│
├── ReporterApp.WindowApp/     # WPF приложение (точка входа)
│   ├── App.xaml(.cs)          # Точка входа, настройка при старте
│   ├── Configuration/
│   │   └── MainWindowConfigurationService.cs
│   ├── Pages/
│   │   └── NewDesign/
│   │       ├── CarNumberPage/
│   │       ├── FileManagementPage/
│   │       ├── ReportPage/
│   │       └── StartPage/
│   ├── Windows/
│   │   ├── Main/
│   │   │   └── MainWindow.xaml
│   │   ├── AddInfoDialog.xaml
│   │   ├── FuelDialog.xaml
│   │   ├── ParkingDialog.xaml
│   │   └── PictureDialog.xaml
│   ├── Resources/
│   │   └── Theme.xaml         # Стили приложения
│   └── window.config.json     # Конфигурация окна
│
└── ReporterApp.UnitTests/     # Модульные тесты
    ├── Core/
    │   ├── Cars/
    │   │   ├── CarEnumeratorTests.cs
    │   │   └── CarUtilsTests.cs
    │   └── Reports/
    └── WindowApp/
```

## Сборка и запуск

### Требования
- .NET 10.0 SDK
- Windows (для WPF)

### Команды

```bash
# Сборка решения
dotnet build ReporterApp.slnx

# Запуск приложения
dotnet run --project ReporterApp.WindowApp/ReporterApp.WindowApp.csproj

# Запуск тестов
dotnet test ReporterApp.UnitTests/ReporterApp.UnitTests.csproj

# Запуск тестов с покрытием
dotnet test --collect:"XPlat Code Coverage"
```

## Архитектурные особенности

### MVVM Паттерн
- `ObservableObject` в Core предоставляет базовую реализацию `INotifyPropertyChanged`
- Модель `Car` наследуется от `ObservableObject` для data binding
- Используются свойства с уведомлением об изменении

### Конфигурация
- `window.config.json` хранит положение, размер и состояние окна
- При старте приложение автоматически открывает веб-страницу (по умолчанию: `https://hosting.wln-hst.com/`)
- Конфигурация сохраняется при выходе из приложения

### Файловая структура
```
%APPDATA%/ReporterApp/
├── data/           # Папка для данных приложения
└── CarsNumbers.txt # Файл с номерами автомобилей
```

## Практики разработки

### Код
- Включены nullable reference types (`<Nullable>enable</Nullable>`)
- Включены implicit usings (`<ImplicitUsings>enable</ImplicitUsings>`)
- Современный C# с pattern matching и collection expressions (`[.. _parking]`)

### Тестирование
- xUnit для модульных тестов
- Moq для моков
- AutoFixture для генерации тестовых данных
- Теория с `[Theory]` и `[InlineData]` для параметризированных тестов

### Стиль кода
- Файлы называются по классу внутри
- Использование `var` для локальных переменных
- Region-комментарии не используются
- Минимальные комментарии в коде

## Ключевые компоненты

### Car (модель)
```csharp
public class Car : ObservableObject
{
    public string Number { get; set; }
    public bool IsWorked { get; set; }
    public int FuelBegin { get; set; }
    public int FuelEnd { get; set; }
    public bool WasScreen { get; set; }
    public bool Was24kmET { get; set; }
    public List<int> Parking { get; set; }
    public List<string> AddInformation { get; set; }
}
```

### CarUtils
```csharp
// Проверка корректности заправки автомобиля
public static Func<Car, bool> IsCarWasFueled = (c) =>
    c.FuelEnd > 0 && c.FuelBegin >= 0 && c.FuelEnd > c.FuelBegin ||
    c.FuelBegin > 0 && c.FuelEnd == 0;
```

## Известные ограничения (v0.1 alpha)

- Не реализована страница со списком автомобилей (Car page ListView)
- Приложение в ранней стадии разработки

## Скриншоты

Скриншоты приложения доступны в папке `Imgs/`:
- `im_1.png` — основной интерфейс
- `im_2.png` — дополнительный вид
