using DAL.FileAccess;
using ReporterApp.DAL.FileAccess;
using System.Text.Json;

namespace ReporterApp.UnitTests.DAL.FileAccess;

public class CarsFileWriterTests : IDisposable
{
    private readonly string _testDirectory;

    public CarsFileWriterTests()
    {
        _testDirectory = Path.Combine(Path.GetTempPath(), $"ReporterAppWriterTests_{Guid.NewGuid()}");
        Directory.CreateDirectory(_testDirectory);
    }

    public void Dispose()
    {
        if (Directory.Exists(_testDirectory))
        {
            Directory.Delete(_testDirectory, true);
        }
    }

    #region WriteJson Tests

    [Fact]
    public void WriteJson_ValidCarsList_WritesJsonToFile()
    {
        // Arrange
        var writer = new CarsFileWriter();
        var cars = new List<Car>
        {
            new Car { Number = "A123BC", IsWorked = true, FuelBegin = 10, FuelEnd = 50 },
            new Car { Number = "X456YX", IsWorked = false }
        };
        var filePath = Path.Combine(_testDirectory, "test_output.json");

        // Act
        var result = writer.WriteJson(cars, filePath);

        // Assert
        Assert.Equal(FileOperationResult.Success, result.Result);
        Assert.True(File.Exists(filePath));

        var content = File.ReadAllText(filePath);
        var deserializedCars = JsonSerializer.Deserialize<List<Car>>(content);
        Assert.Equal(2, deserializedCars?.Count);
        Assert.Equal("A123BC", deserializedCars[0].Number);
        Assert.Equal("X456YX", deserializedCars[1].Number);
    }

    [Fact]
    public void WriteJson_EmptyCarsList_WritesEmptyArray()
    {
        // Arrange
        var writer = new CarsFileWriter();
        var cars = new List<Car>();
        var filePath = Path.Combine(_testDirectory, "empty_output.json");

        // Act
        var result = writer.WriteJson(cars, filePath);

        // Assert
        Assert.Equal(FileOperationResult.Success, result.Result);
        Assert.True(File.Exists(filePath));

        var content = File.ReadAllText(filePath);
        Assert.Equal("[]", content);
    }

    [Fact]
    public void WriteJson_InvalidPath_ReturnsErrorResult()
    {
        // Arrange
        var writer = new CarsFileWriter();
        var cars = new List<Car> { new Car { Number = "A123BC" } };
        var invalidPath = "Z:\\NonExistentDrive\\test.json";

        // Act
        var result = writer.WriteJson(cars, invalidPath);

        // Assert
        Assert.Equal(FileOperationResult.Error, result.Result);
        Assert.Contains("Error:", result.Message);
    }

    [Fact]
    public void WriteJson_CarWithAllProperties_SerializesAllProperties()
    {
        // Arrange
        var writer = new CarsFileWriter();
        var cars = new List<Car>
        {
            new Car
            {
                Number = "A123BC",
                IsWorked = true,
                FuelBegin = 10,
                FuelEnd = 50,
                WasScreen = true,
                Was24kmET = true,
                Parking = new List<int> { 10, 20 },
                AddInformation = new List<string> { "Инфо 1" }
            }
        };
        var filePath = Path.Combine(_testDirectory, "full_car.json");

        // Act
        var result = writer.WriteJson(cars, filePath);

        // Assert
        Assert.Equal(FileOperationResult.Success, result.Result);

        var content = File.ReadAllText(filePath);
        var deserializedCars = JsonSerializer.Deserialize<List<Car>>(content);
        var car = deserializedCars?[0];
        Assert.NotNull(car);
        Assert.True(car.IsWorked);
        Assert.Equal(10, car.FuelBegin);
        Assert.Equal(50, car.FuelEnd);
        Assert.True(car.WasScreen);
        Assert.True(car.Was24kmET);
        Assert.Equal(new List<int> { 10, 20 }, car.Parking);
        Assert.Equal(new List<string> { "Инфо 1" }, car.AddInformation);
    }

    [Fact]
    public void WriteJson_OverwritesExistingFile_OverwritesContent()
    {
        // Arrange
        var writer = new CarsFileWriter();
        var filePath = Path.Combine(_testDirectory, "overwrite_test.json");

        // Write initial content
        var initialCars = new List<Car> { new Car { Number = "OLD" } };
        writer.WriteJson(initialCars, filePath);

        // Act - overwrite with new content
        var newCars = new List<Car> { new Car { Number = "NEW1" }, new Car { Number = "NEW2" } };
        var result = writer.WriteJson(newCars, filePath);

        // Assert
        Assert.Equal(FileOperationResult.Success, result.Result);

        var content = File.ReadAllText(filePath);
        var deserializedCars = JsonSerializer.Deserialize<List<Car>>(content);
        Assert.Equal(2, deserializedCars?.Count);
        Assert.Equal("NEW1", deserializedCars[0].Number);
        Assert.Equal("NEW2", deserializedCars[1].Number);
        Assert.DoesNotContain("OLD", content);
    }

    #endregion

    #region WriteCarNumbers Tests

    [Fact]
    public void WriteCarNumbers_ValidCarsList_WritesNumbersToFile()
    {
        // Arrange
        var writer = new CarsFileWriter();
        var cars = new List<Car>
        {
            new Car { Number = "A123BC" },
            new Car { Number = "X456YX" },
            new Car { Number = "B789CC" }
        };
        var filePath = Path.Combine(_testDirectory, "numbers_output.txt");

        // Act
        var result = writer.WriteCarNumbers(cars, filePath);

        // Assert
        Assert.Equal(FileOperationResult.Success, result.Result);
        Assert.True(File.Exists(filePath));

        var content = File.ReadAllText(filePath);
        var lines = content.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        Assert.Equal(3, lines.Length);
        Assert.Equal("A123BC", lines[0]);
        Assert.Equal("X456YX", lines[1]);
        Assert.Equal("B789CC", lines[2]);
    }

    [Fact]
    public void WriteCarNumbers_EmptyCarsList_WritesEmptyFile()
    {
        // Arrange
        var writer = new CarsFileWriter();
        var cars = new List<Car>();
        var filePath = Path.Combine(_testDirectory, "empty_numbers.txt");

        // Act
        var result = writer.WriteCarNumbers(cars, filePath);

        // Assert
        Assert.Equal(FileOperationResult.Success, result.Result);
        Assert.True(File.Exists(filePath));

        var content = File.ReadAllText(filePath);
        Assert.Equal(string.Empty, content);
    }

    [Fact]
    public void WriteCarNumbers_SingleCar_WritesSingleNumber()
    {
        // Arrange
        var writer = new CarsFileWriter();
        var cars = new List<Car> { new Car { Number = "A123BC" } };
        var filePath = Path.Combine(_testDirectory, "single_number.txt");

        // Act
        var result = writer.WriteCarNumbers(cars, filePath);

        // Assert
        Assert.Equal(FileOperationResult.Success, result.Result);

        var content = File.ReadAllText(filePath);
        Assert.Equal("A123BC", content);
    }

    [Fact]
    public void WriteCarNumbers_InvalidPath_ReturnsErrorResult()
    {
        // Arrange
        var writer = new CarsFileWriter();
        var cars = new List<Car> { new Car { Number = "A123BC" } };
        var invalidPath = "Z:\\NonExistentDrive\\numbers.txt";

        // Act
        var result = writer.WriteCarNumbers(cars, invalidPath);

        // Assert
        Assert.Equal(FileOperationResult.Error, result.Result);
        Assert.Contains("Error:", result.Message);
    }

    [Fact]
    public void WriteCarNumbers_CarsWithEmptyNumbers_WritesEmptyLines()
    {
        // Arrange
        var writer = new CarsFileWriter();
        var cars = new List<Car>
        {
            new Car { Number = "A123BC" },
            new Car { Number = string.Empty },
            new Car { Number = "X456YX" }
        };
        var filePath = Path.Combine(_testDirectory, "with_empty.txt");

        // Act
        var result = writer.WriteCarNumbers(cars, filePath);

        // Assert
        Assert.Equal(FileOperationResult.Success, result.Result);

        var content = File.ReadAllText(filePath);
        var lines = content.Split("\r\n", StringSplitOptions.None);
        Assert.Equal(3, lines.Length);
        Assert.Equal("A123BC", lines[0]);
        Assert.Equal(string.Empty, lines[1]);
        Assert.Equal("X456YX", lines[2]);
    }

    [Fact]
    public void WriteCarNumbers_OverwritesExistingFile_OverwritesContent()
    {
        // Arrange
        var writer = new CarsFileWriter();
        var filePath = Path.Combine(_testDirectory, "overwrite_numbers.txt");

        // Write initial content
        var initialCars = new List<Car> { new Car { Number = "OLD" } };
        writer.WriteCarNumbers(initialCars, filePath);

        // Act - overwrite with new content
        var newCars = new List<Car>
        {
            new Car { Number = "NEW1" },
            new Car { Number = "NEW2" }
        };
        var result = writer.WriteCarNumbers(newCars, filePath);

        // Assert
        Assert.Equal(FileOperationResult.Success, result.Result);

        var content = File.ReadAllText(filePath);
        var lines = content.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        Assert.Equal(2, lines.Length);
        Assert.Equal("NEW1", lines[0]);
        Assert.Equal("NEW2", lines[1]);
        Assert.DoesNotContain("OLD", content);
    }

    #endregion
}
