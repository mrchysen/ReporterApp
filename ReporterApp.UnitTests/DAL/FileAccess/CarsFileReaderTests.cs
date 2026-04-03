using ReporterApp.DAL.FileAccess;
using System.Text.Json;

namespace ReporterApp.UnitTests.DAL.FileAccess;

public class CarsFileReaderTests : IDisposable
{
    private readonly string _testDirectory;
    private readonly string _jsonFilePath;
    private readonly string _txtFilePath;

    public CarsFileReaderTests()
    {
        _testDirectory = Path.Combine(Path.GetTempPath(), $"ReporterAppTests_{Guid.NewGuid()}");
        Directory.CreateDirectory(_testDirectory);
        _jsonFilePath = Path.Combine(_testDirectory, "test_cars.json");
        _txtFilePath = Path.Combine(_testDirectory, "test_cars.txt");
    }

    public void Dispose()
    {
        if (Directory.Exists(_testDirectory))
        {
            Directory.Delete(_testDirectory, true);
        }
    }

    #region ReadJson Tests

    [Fact]
    public void ReadJson_FileDoesNotExist_ReturnsInfoResult()
    {
        // Arrange
        var reader = new CarsFileReader();
        var nonExistentPath = Path.Combine(_testDirectory, "nonexistent.json");

        // Act
        var result = reader.ReadJson(nonExistentPath);

        // Assert
        Assert.Equal(FileOperationResult.Info, result.Result);
        Assert.Empty(result.Cars);
        Assert.Contains("не нашлось", result.Message);
    }

    [Fact]
    public void ReadJson_ValidJsonFile_ReturnsCarsList()
    {
        // Arrange
        var cars = new List<Car>
        {
            new Car { Number = "A123BC", IsWorked = true, FuelBegin = 10, FuelEnd = 50 },
            new Car { Number = "X456YX", IsWorked = false }
        };
        File.WriteAllText(_jsonFilePath, JsonSerializer.Serialize(cars));
        var reader = new CarsFileReader();

        // Act
        var result = reader.ReadJson(_jsonFilePath);

        // Assert
        Assert.Equal(FileOperationResult.Success, result.Result);
        Assert.Equal(2, result.Cars.Count);
        Assert.Equal("A123BC", result.Cars[0].Number);
        Assert.Equal("X456YX", result.Cars[1].Number);
    }

    [Fact]
    public void ReadJson_EmptyJsonFile_ReturnsEmptyList()
    {
        // Arrange
        File.WriteAllText(_jsonFilePath, "[]");
        var reader = new CarsFileReader();

        // Act
        var result = reader.ReadJson(_jsonFilePath);

        // Assert
        Assert.Equal(FileOperationResult.Success, result.Result);
        Assert.Empty(result.Cars);
    }

    [Fact]
    public void ReadJson_InvalidJson_ReturnsErrorResult()
    {
        // Arrange
        File.WriteAllText(_jsonFilePath, "{ invalid json }");
        var reader = new CarsFileReader();

        // Act
        var result = reader.ReadJson(_jsonFilePath);

        // Assert
        Assert.Equal(FileOperationResult.Error, result.Result);
        Assert.Contains("Error:", result.Message);
    }

    [Fact]
    public void ReadJson_NullInJson_ReturnsEmptyList()
    {
        // Arrange
        File.WriteAllText(_jsonFilePath, "null");
        var reader = new CarsFileReader();

        // Act
        var result = reader.ReadJson(_jsonFilePath);

        // Assert
        Assert.Equal(FileOperationResult.Success, result.Result);
        Assert.Empty(result.Cars);
    }

    [Fact]
    public void ReadJson_PartialCarData_DeserializesWithDefaultValues()
    {
        // Arrange
        var json = """[{"Number": "A111AA"}]""";
        File.WriteAllText(_jsonFilePath, json);
        var reader = new CarsFileReader();

        // Act
        var result = reader.ReadJson(_jsonFilePath);

        // Assert
        Assert.Equal(FileOperationResult.Success, result.Result);
        Assert.Single(result.Cars);
        Assert.Equal("A111AA", result.Cars[0].Number);
        Assert.False(result.Cars[0].IsWorked);
        Assert.Equal(0, result.Cars[0].FuelBegin);
    }

    #endregion

    #region ReadOnlyNumbers Tests

    [Fact]
    public void ReadOnlyNumbers_FileDoesNotExist_ReturnsInfoResult()
    {
        // Arrange
        var reader = new CarsFileReader();
        var nonExistentPath = Path.Combine(_testDirectory, "nonexistent.txt");

        // Act
        var result = reader.ReadOnlyNumbers(nonExistentPath);

        // Assert
        Assert.Equal(FileOperationResult.Info, result.Result);
        Assert.Empty(result.Cars);
    }

    [Fact]
    public void ReadOnlyNumbers_ValidFile_ReturnsCarsWithNumbers()
    {
        // Arrange
        var content = "A123BC\r\nX456YX\r\nB789CC";
        File.WriteAllText(_txtFilePath, content);
        var reader = new CarsFileReader();

        // Act
        var result = reader.ReadOnlyNumbers(_txtFilePath);

        // Assert
        Assert.Equal(FileOperationResult.Success, result.Result);
        Assert.Equal(3, result.Cars.Count);
        Assert.Equal("A123BC", result.Cars[0].Number);
        Assert.Equal("X456YX", result.Cars[1].Number);
        Assert.Equal("B789CC", result.Cars[2].Number);
    }

    [Fact]
    public void ReadOnlyNumbers_EmptyFile_ReturnsInfoResult()
    {
        // Arrange
        File.WriteAllText(_txtFilePath, string.Empty);
        var reader = new CarsFileReader();

        // Act
        var result = reader.ReadOnlyNumbers(_txtFilePath);

        // Assert
        Assert.Equal(FileOperationResult.Info, result.Result);
        Assert.Empty(result.Cars);
        Assert.Contains("нет номеров", result.Message);
    }

    [Fact]
    public void ReadOnlyNumbers_FileWithEmptyLines_SkipsEmptyLines()
    {
        // Arrange
        var content = "A123BC\r\n\r\nX456YX\r\n\r\n";
        File.WriteAllText(_txtFilePath, content);
        var reader = new CarsFileReader();

        // Act
        var result = reader.ReadOnlyNumbers(_txtFilePath);

        // Assert
        Assert.Equal(FileOperationResult.Success, result.Result);
        Assert.Equal(2, result.Cars.Count);
    }

    [Fact]
    public void ReadOnlyNumbers_FileWithWhitespaceLines_IncludesWhitespaceAsNumber()
    {
        // Arrange
        var content = "A123BC\r\n   \r\nX456YX";
        File.WriteAllText(_txtFilePath, content);
        var reader = new CarsFileReader();

        // Act
        var result = reader.ReadOnlyNumbers(_txtFilePath);

        // Assert
        Assert.Equal(FileOperationResult.Success, result.Result);
        Assert.Equal(3, result.Cars.Count);
        Assert.Equal("A123BC", result.Cars[0].Number);
        Assert.Equal("   ", result.Cars[1].Number);
        Assert.Equal("X456YX", result.Cars[2].Number);
    }

    [Fact]
    public void ReadOnlyNumbers_SingleNumber_ReturnsSingleCar()
    {
        // Arrange
        File.WriteAllText(_txtFilePath, "A123BC");
        var reader = new CarsFileReader();

        // Act
        var result = reader.ReadOnlyNumbers(_txtFilePath);

        // Assert
        Assert.Equal(FileOperationResult.Success, result.Result);
        Assert.Single(result.Cars);
        Assert.Equal("A123BC", result.Cars[0].Number);
    }

    #endregion
}
