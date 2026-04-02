# План работ по рефакторингу ReporterApp

## Статус проекта
- **Версия:** 0.1 alpha
- **Платформа:** .NET 10.0, WPF
- **Архитектура:** MVVM

---

## 🔴 Критические проблемы

### 1. Архитектурные нарушения

#### 1.1. Смешение слоёв приложения
- **Проблема:** `CarsFileReader` в DAL использует модель `Car` из Core, но сам находится в пространстве имён `DAL.FileAccess` без интерфейса
- **Решение:**
  - Выделить интерфейс `ICarsFileReader`
  - Переместить `CarsFileReader` в корректное пространство имён `ReporterApp.DAL.FileAccess`
  - Добавить регистрацию зависимостей (DI)

#### 1.2. Отсутствие Dependency Injection
- **Проблема:** Зависимости создаются через `new` напрямую в ViewModel и командах
- **Решение:**
  - Добавить Microsoft.Extensions.DependencyInjection
  - Настроить контейнер в `App.xaml.cs`
  - Внедрять зависимости через конструкторы

#### 1.3. ViewModelMediator — "Божественный объект"
- **Проблема:** Класс хранит ссылки на все ViewModel, нарушает SRP
- **Решение:**
  - Удалить `ViewModelMediator`
  - Использовать сервис сообщений (Messenger/EventAggregator)
  - Или переработать навигацию через параметры

---

## ✅ Выполненные задачи

### 0. Интеграция CommunityToolkit.Mvvm

#### 0.1. Добавление пакета
- [x] Добавить `CommunityToolkit.Mvvm` в `ReporterApp.Core`
- [x] Добавить `CommunityToolkit.Mvvm` в `ReporterApp.WindowApp`

#### 0.2. Рефакторинг модели Car
- [x] Переписать `Car.cs` с использованием `[ObservableProperty]`
- [x] Убрать ручной бойлерплейт `INotifyPropertyChanged`

#### 0.3. Рефакторинг ViewModel
- [x] Переписать `CarNumberViewModel` с использованием `CommunityToolkit.Mvvm`
- [x] Переписать `StartPageViewModel` с использованием `CommunityToolkit.Mvvm`
- [x] Переписать `ReportPageViewModel` с использованием `CommunityToolkit.Mvvm`
- [x] Переписать `FileManagementPageViewModel` с использованием `CommunityToolkit.Mvvm`
- [x] Переписать `MainWindowViewModel` с использованием `CommunityToolkit.Mvvm`

#### 0.4. Рефакторинг CarEnumerator
- [x] Переписать `CarEnumerator.cs` с использованием `CommunityToolkit.Mvvm`

---

## 🟡 Структурные улучшения

### 2. Рефакторинг Core слоя

#### 2.1. Car.cs
- [x] Добавить валидацию свойств (DataAnnotations)
- [x] Реализовать `IEquatable<Car>` для сравнения по номеру
- [x] Добавить конструкторы для инициализации
- [x] Рассмотреть использование `ObservableCollection<T>` для коллекций

#### 2.2. CarUtils.cs
- [ ] Расширить покрытие тестами
- [ ] Добавить документацию к методам
- [ ] Рассмотреть выделение в отдельный сервис `ICarValidationService`

#### 2.3. CarEnumerator.cs
- [x] Реализовать `IEnumerator<Car>` полноценно
- [ ] Добавить тесты на граничные условия

### 3. Рефакторинг Reports

#### 3.1. IReportBuilder
- [ ] Добавить метод `Clear()` для сброса состояния билдера
- [ ] Добавить интерфейс `IReportTitleProvider` для заголовков
- [ ] Вынести форматирование строк в отдельные сервисы

#### 3.2. StringBuilderExtension
- [ ] Покрыть тестами
- [ ] Добавить документацию

### 4. Рефакторинг DAL

#### 4.1. Файловый доступ
- [ ] Выделить `ICarsFileReader` интерфейс
- [ ] Добавить асинхронные методы (`ReadJsonAsync`, `WriteJsonAsync`)
- [ ] Добавить обработку transient-ошибок
- [ ] Вынести путь к файлам в конфигурацию

#### 4.2. CarsOperationInfo и FileOperationResult
- [ ] Преобразовать в record types (C# 9+)
- [ ] Добавить pattern matching для обработки результатов
- [ ] Рассмотреть использование `Result<T>` паттерна

---

## 🟢 Улучшения WindowApp

### 5. ViewModel

#### 5.1. Базовый класс
- [x] Добавить `SetProperty` метод в `ObservableObject` для уменьшения бойлерплейта
- [x] Добавить поддержку async команд (`AsyncRelayCommand`)
- [x] Рассмотреть использование CommunityToolkit.Mvvm

#### 5.2. CarNumberViewModel
- [ ] Исправить чтение строк (`\r\n` не кроссплатформенно)
- [ ] Добавить валидацию номеров машин
- [ ] Добавить отмену операции (ICommand.CanExecute)

#### 5.3. StartPageViewModel
- [ ] Убрать зависимость от `PageNavigatorService`
- [ ] Использовать навигацию через сообщения

### 6. Команды

#### 6.1. ReportChooseCommand<T>
- [ ] Убрать дженерик-ограничение `new()`
- [ ] Добавить `ICommand` фабрику
- [ ] Покрыть тестами

#### 6.2. SaveCommand
- [ ] Добавить асинхронную версию (`IAsyncCommand`)
- [ ] Добавить обработку ошибок с уведомлением пользователя
- [ ] Добавить подтверждение при потере данных

### 7. Конфигурация

#### 7.1. FilesConfiguration
- [ ] Переместить в Core или выделить в отдельный проект `ReporterApp.Configuration`
- [ ] Сделать конфигурацию injectable (`IConfigurationProvider`)
- [ ] Добавить валидацию путей

#### 7.2. MainWindowConfigurationService
- [ ] Добавить интерфейс `IWindowConfigurationService`
- [ ] Вынести магические строки в константы
- [ ] Добавить обработку битой конфигурации

---

## 🔵 Тестирование

### 8. Покрытие тестами

#### 8.1. Приоритет 1 (Core)
- [ ] `Car` — тесты на свойства и уведомления
- [ ] `CarUtils.IsCarWasFueled` — все комбинации
- [ ] `CarEnumerator` — полный цикл, граничные условия
- [ ] `DefaultReportBuilder` — все сценарии отчётов
- [ ] `GasReportBuilder` — тесты
- [ ] `GasAndDefaultReportBuilder` — тесты

#### 8.2. Приоритет 2 (DAL)
- [ ] `CarsFileReader.ReadJson` — все сценарии
- [ ] `CarsFileReader.ReadOnlyNumbers` — все сценарии
- [ ] `CarsFileWriter.WriteJson` — все сценарии
- [ ] `CarsFileWriter.WriteCarNumbers` — все сценарии
- [ ] Тесты на асинхронные методы (когда будут добавлены)

#### 8.3. Приоритет 3 (WindowApp)
- [ ] `SaveCommand.Execute` — логика слияния
- [ ] `ReportChooseCommand.Execute` — навигация
- [ ] `CarNumberViewModel` — логика без UI
- [ ] `ViewModelMediator` — координация

#### 8.4. Интеграционные тесты
- [ ] Полный цикл: создание машины → сохранение → чтение → отчёт
- [ ] Тесты конфигурации окна

### 9. Улучшение тестов
- [ ] Добавить AutoFixture для генерации данных
- [ ] Выделить базовые классы для тестов с временными файлами
- [ ] Добавить тестовые данные (fixtures)

---

## 🟣 UI/UX улучшения

### 10. WPF компоненты

#### 10.1. Стили
- [ ] Вынести цвета в отдельный ресурс (Theme.xaml уже есть)
- [ ] Добавить поддержку тёмной темы
- [ ] Унифицировать отступы и размеры

#### 10.2. Диалоги
- [ ] `FuelDialog` — добавить валидацию
- [ ] `ParkingDialog` — добавить валидацию
- [ ] `AddInfoDialog` — добавить валидацию
- [ ] `PictureDialog` — добавить обработку ошибок

#### 10.3. Страницы
- [ ] **Car Page (ListView)** — НЕ РЕАЛИЗОВАНО (критично для v0.2)
- [ ] `ReportPage` — добавить предпросмотр
- [ ] `FileManagementPage` — улучшить UX выбора даты

---

## 🟤 Производительность

### 11. Оптимизация

#### 11.1. Работа с файлами
- [ ] Перейти на асинхронное чтение/запись
- [ ] Добавить кэширование прочитанных данных
- [ ] Оптимизировать сериализацию (System.Text.Json уже используется)

#### 11.2. UI производительность
- [ ] Добавить виртуализацию для больших списков
- [ ] Оптимизировать binding (UpdateSourceTrigger)
- [ ] Избегать утечек памяти в подписках на события

---

## ⚫ Документация

### 12. Документирование

#### 12.1. Код
- [ ] Добавить XML-документацию к публичным API
- [ ] Добавить комментарии к сложной логике
- [ ] Обновить README с инструкцией по сборке

#### 12.2. Архитектура
- [ ] Добавить диаграмму классов
- [ ] Описать жизненный цикл приложения
- [ ] Документировать MVVM связи

---

## 📋 Дорожная карта

### Версия 0.2 (ближайшая)
1. ✅ Реализовать Car Page (ListView автомобилей)
2. ✅ Добавить базовые тесты для Core
3. ✅ Исправить критические архитектурные проблемы

### Версия 0.3
1. ✅ Внедрить Dependency Injection
2. ✅ Удалить ViewModelMediator
3. ✅ Добавить асинхронные операции

### Версия 0.4
1. ✅ Полное покрытие тестами Core и DAL
2. ✅ Улучшить UI/UX
3. ✅ Добавить поддержку тем

### Версия 1.0
1. ✅ Стабильный релиз
2. ✅ Полная документация
3. ✅ Оптимизация производительности

---

## 🛠 Инструменты для рефакторинга

### Рекомендуемые NuGet пакеты
```xml
<PackageReference Include="CommunityToolkit.Mvvm" Version="8.2.0" />
<PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="8.0.0" />
<PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="8.0.0" />
```

### Для тестов
```xml
<PackageReference Include="xunit" Version="2.6.0" />
<PackageReference Include="xunit.runner.visualstudio" Version="2.6.0" />
<PackageReference Include="Moq" Version="4.20.0" />
<PackageReference Include="AutoFixture" Version="4.18.0" />
<PackageReference Include="coverlet.collector" Version="6.0.0" />
```

### Анализ кода
```bash
# Запуск анализатора
dotnet build /warnaserror

# Проверка покрытия
dotnet test --collect:"XPlat Code Coverage"

# Анализ зависимостей
dotnet list package --outdated
```

---

## 📝 Примечания

- Рефакторинг проводить итеративно
- Каждый пункт должен сопровождаться тестами
- Не ломать существующую функциональность
- Сохранять обратную совместимость данных
