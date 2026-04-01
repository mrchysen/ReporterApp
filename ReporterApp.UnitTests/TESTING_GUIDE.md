# Руководство по написанию тестов для ReporterApp

## 📁 Структура тестов

### Организация тестовых файлов

```
ReporterApp.UnitTests/
├── Core/                    # Тесты для слоя Core (бизнес-логика)
│   ├── Cars/
│   │   ├── CarTests.cs
│   │   ├── CarEnumeratorTests.cs
│   │   └── CarUtilsTests.cs
│   └── Reports/
│       ├── DefaultReportBuilderTests.cs
│       ├── GasReportBuilderTests.cs
│       └── ...
├── DAL/                     # Тесты для слоя доступа к данным
│   └── FileAccess/
│       ├── CarsFileReaderTests.cs
│       └── CarsFileWriterTests.cs
└── WindowApp/               # Тесты для UI слоя (ограниченно)
    └── Commands/
        ├── WorkStatusChangeCommandTests.cs
        └── NavigationCommandTests.cs
```

## ✅ Правила написания тестов

### 1. Структура тестового класса

**✅ ПРАВИЛЬНО:** Один класс на тестируемый класс, методы сгруппированы по регионам:

```csharp
public class CarsFileReaderTests : IDisposable
{
    // Общие поля и конструктор
    
    #region ReadJson Tests
    
    [Fact]
    public void ReadJson_FileDoesNotExist_ReturnsInfoResult()
    { ... }
    
    [Fact]
    public void ReadJson_ValidJsonFile_ReturnsCarsList()
    { ... }
    
    #endregion
    
    #region ReadOnlyNumbers Tests
    
    [Fact]
    public void ReadOnlyNumbers_ValidFile_ReturnsCarsWithNumbers()
    { ... }
    
    #endregion
}
```

**❌ НЕПРАВИЛЬНО:** Вложенные классы для каждого метода:

```csharp
// ❌ Избегайте такой структуры!
public class CarsFileReaderTests : IDisposable
{
    public class ReadJsonTests : CarsFileReaderTests
    {
        [Fact]
        public void ReadJson_FileDoesNotExist_ReturnsInfoResult()
        { ... }
    }
    
    public class ReadOnlyNumbersTests : CarsFileReaderTests
    {
        [Fact]
        public void ReadOnlyNumbers_ValidFile_ReturnsCarsWithNumbers()
        { ... }
    }
}
```

**Почему:**
- Вложенные классы усложняют навигацию
- Дублирование кода setup/teardown
- Сложнее рефакторить
- Нарушает принцип "один класс = один файл"

### 2. Именование тестов

**✅ ПРАВИЛЬНО:** Используйте паттерн `Method_Condition_ExpectedResult`:

```csharp
[Fact]
public void ReadJson_FileDoesNotExist_ReturnsInfoResult()
[Fact]
public void WriteJson_InvalidPath_ReturnsErrorResult()
[Fact]
public void Execute_IsWorkedFalse_TogglesToTrue()
```

**❌ НЕПРАВИЛЬНО:** Слишком короткие или неясные имена:

```csharp
[Fact]
public void Test1()
[Fact]
public void ReadJsonTest()
[Fact]
public void ShouldWork()
```

### 3. Структура теста (AAA Pattern)

Каждый тест должен следовать паттерну **Arrange-Act-Assert**:

```csharp
[Fact]
public void WriteJson_ValidCarsList_WritesJsonToFile()
{
    // Arrange
    var writer = new CarsFileWriter();
    var cars = new List<Car> { ... };
    var filePath = Path.Combine(_testDirectory, "test.json");

    // Act
    var result = writer.WriteJson(cars, filePath);

    // Assert
    Assert.Equal(FileOperationResult.Success, result.Result);
    Assert.True(File.Exists(filePath));
}
```

### 4. Работа с ресурсами

Для тестов, работающих с файлами, используйте `IDisposable`:

```csharp
public class CarsFileWriterTests : IDisposable
{
    private readonly string _testDirectory;

    public CarsFileWriterTests()
    {
        _testDirectory = Path.Combine(
            Path.GetTempPath(), 
            $"ReporterAppTests_{Guid.NewGuid()}"
        );
        Directory.CreateDirectory(_testDirectory);
    }

    public void Dispose()
    {
        if (Directory.Exists(_testDirectory))
        {
            Directory.Delete(_testDirectory, true);
        }
    }
}
```

### 5. Избегайте тестов, требующих UI потока

**❌ НЕПРАВИЛЬНО:** Тесты, создающие WPF контролы:

```csharp
// ❌ Будет работать только в STA потоке
[Fact]
public void SomeTest()
{
    var frame = new Frame(); // Требует UI потока
    var viewModel = new ViewModel(frame);
    ...
}
```

**✅ ПРАВИЛЬНО:** Тестируйте только бизнес-логику:

```csharp
[Fact]
public void Execute_ChangesIsWorkedStatus()
{
    var cars = new List<Car> { new Car { IsWorked = false } };
    var enumerator = new CarEnumerator(cars);
    var command = new WorkStatusChangeCommand(enumerator);
    
    command.Execute(null);
    
    Assert.True(cars[0].IsWorked);
}
```

### 6. Mock объекты

Используйте Moq осторожно. Избегайте моков для:
- Классов без виртуальных методов (будет ошибка)
- WPF ViewModel с не-virtual свойствами

**✅ ПРАВИЛЬНО:**

```csharp
var mockFileWriter = new Mock<ICarsFileWriter>();
mockFileWriter
    .Setup(w => w.WriteCarNumbers(It.IsAny<List<Car>>(), It.IsAny<string>()))
    .Returns(new CarsOperationInfo { Result = FileOperationResult.Success });
```

**❌ НЕПРАВИЛЬНО:**

```csharp
// ❌ Не-virtual свойства не могут быть замокированы
var mockViewModel = new Mock<ReportPageViewModel>();
mockViewModel.SetupGet(v => v.Cars).Returns(cars); // Ошибка!
```

### 7. Покрытие тестами

Приоритеты покрытия:

1. **Core слой** (100% покрытие)
   - Модели данных
   - Бизнес-логика
   - Утилиты

2. **DAL слой** (80%+ покрытие)
   - Чтение/запись файлов
   - Обработка ошибок

3. **WindowApp слой** (избранные тесты)
   - Команды (без UI зависимостей)
   - Простые ViewModel

### 8. Группировка тестов

Используйте регионы для группировки по методам:

```csharp
#region ReadJson Tests
// Тесты для метода ReadJson
#endregion

#region ReadOnlyNumbers Tests
// Тесты для метода ReadOnlyNumbers
#endregion

#region WriteJson Tests
// Тесты для метода WriteJson
#endregion

#region WriteCarNumbers Tests
// Тесты для метода WriteCarNumbers
#endregion
```

## 🚀 Быстрый старт

### Добавить новый тест:

1. Найдите существующий тестовый файл для вашего класса
2. Добавьте новый метод с `[Fact]` или `[Theory]`
3. Следуйте AAA паттерну
4. Используйте описательное имя

### Пример добавления теста:

```csharp
[Fact]
public void ReadJson_EmptyJsonFile_ReturnsEmptyList()
{
    // Arrange
    File.WriteAllText(_testFilePath, "[]");
    var reader = new CarsFileReader();

    // Act
    var result = reader.ReadJson(_testFilePath);

    // Assert
    Assert.Equal(FileOperationResult.Success, result.Result);
    Assert.Empty(result.Cars);
}
```

## 📚 Используемые пакеты

- **xUnit** — фреймворк для тестирования
- **Moq** — мокирование зависимостей
- **AutoFixture** — генерация тестовых данных (опционально)

## 🔍 Запуск тестов

```bash
# Запустить все тесты
dotnet test

# Запустить с подробным выводом
dotnet test --verbosity normal

# Запустить конкретный тест
dotnet test --filter "FullyQualifiedName~CarsFileReaderTests"

# Запустить с покрытием кода
dotnet test /p:CollectCoverage=true
```
