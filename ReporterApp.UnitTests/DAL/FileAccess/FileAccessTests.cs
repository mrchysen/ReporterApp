using DAL.FileAccess;

namespace ReporterApp.UnitTests.DAL.FileAccess;

/// <summary>
/// Тесты для enum FileOperationResult.
/// </summary>
public class FileOperationResultTests
{
    [Fact]
    public void FileOperationResult_EnumValues_AreDefined()
    {
        // Arrange & Act
        var values = Enum.GetNames(typeof(FileOperationResult));

        // Assert
        Assert.Contains("Success", values);
        Assert.Contains("Error", values);
        Assert.Contains("Info", values);
    }

    [Fact]
    public void FileOperationResult_Success_HasValue0()
    {
        // Assert
        Assert.Equal(0, (int)FileOperationResult.Success);
    }

    [Fact]
    public void FileOperationResult_Error_HasValue1()
    {
        // Assert
        Assert.Equal(1, (int)FileOperationResult.Error);
    }

    [Fact]
    public void FileOperationResult_Info_HasValue2()
    {
        // Assert
        Assert.Equal(2, (int)FileOperationResult.Info);
    }
}

/// <summary>
/// Тесты для класса CarsOperationInfo.
/// </summary>
public class CarsOperationInfoTests
{
    [Fact]
    public void Constructor_DefaultValues_CorrectDefaults()
    {
        // Arrange & Act
        var info = new CarsOperationInfo();

        // Assert
        Assert.Equal(FileOperationResult.Success, info.Result);
        Assert.Empty(info.Cars);
        Assert.Equal(string.Empty, info.Message);
    }

    [Fact]
    public void Result_SetValue_PropertyUpdated()
    {
        // Arrange
        var info = new CarsOperationInfo();

        // Act
        info.Result = FileOperationResult.Error;

        // Assert
        Assert.Equal(FileOperationResult.Error, info.Result);
    }

    [Fact]
    public void Cars_SetValue_PropertyUpdated()
    {
        // Arrange
        var info = new CarsOperationInfo();
        var cars = new List<Car> { new Car { Number = "A123BC" } };

        // Act
        info.Cars = cars;

        // Assert
        Assert.Equal(cars, info.Cars);
        Assert.Single(info.Cars);
    }

    [Fact]
    public void Message_SetValue_PropertyUpdated()
    {
        // Arrange
        var info = new CarsOperationInfo();

        // Act
        info.Message = "Test message";

        // Assert
        Assert.Equal("Test message", info.Message);
    }

    [Fact]
    public void ToString_SuccessResult_FormattedCorrectly()
    {
        // Arrange
        var info = new CarsOperationInfo
        {
            Result = FileOperationResult.Success,
            Message = "Всё хорошо"
        };

        // Act
        var result = info.ToString();

        // Assert
        Assert.Equal("Success: Всё хорошо", result);
    }

    [Fact]
    public void ToString_ErrorResult_FormattedCorrectly()
    {
        // Arrange
        var info = new CarsOperationInfo
        {
            Result = FileOperationResult.Error,
            Message = "Ошибка записи"
        };

        // Act
        var result = info.ToString();

        // Assert
        Assert.Equal("Error: Ошибка записи", result);
    }

    [Fact]
    public void ToString_InfoResult_FormattedCorrectly()
    {
        // Arrange
        var info = new CarsOperationInfo
        {
            Result = FileOperationResult.Info,
            Message = "Файл не найден"
        };

        // Act
        var result = info.ToString();

        // Assert
        Assert.Equal("Info: Файл не найден", result);
    }
}
