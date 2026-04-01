using ReporterApp.Core.Reports;

namespace ReporterApp.UnitTests.Core.Reports;

public class GasAndDefaultReportBuilderTests
{
    [Fact]
    public void AddBodyReportText_EmptyCarsList_EmptyReport()
    {
        // Arrange
        var reportBuilder = new GasAndDefaultReportBuilder();
        var cars = new List<Car>();
        var date = new DateTime(2025, 5, 10);

        // Act
        var result = reportBuilder.AddBodyReportText(cars, date, false).GetReport();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void AddBodyReportText_WithCars_CombinesBothReports()
    {
        // Arrange
        var reportBuilder = new GasAndDefaultReportBuilder();
        var cars = new List<Car>
        {
            new Car { Number = "111", IsWorked = true, FuelBegin = 10, FuelEnd = 30 }
        };
        var date = new DateTime(2025, 5, 10); // Суббота

        // Act
        var result = reportBuilder.AddBodyReportText(cars, date, true).GetReport();

        // Assert
        Assert.Contains("Топливный + стандартный отчёт", result);
        Assert.Contains("111 заправили с 10 до 30 литров(20), ок.", result);
        Assert.Contains("111 заправка: с 10 до 30 (20 литров).", result);
    }

    [Fact]
    public void AddBodyReportText_WithAddTitle_HasCorrectTitle()
    {
        // Arrange
        var reportBuilder = new GasAndDefaultReportBuilder();
        var cars = new List<Car>
        {
            new Car { Number = "111", IsWorked = true }
        };
        var date = new DateTime(2025, 5, 10); // Суббота

        var expectedTitle = "Топливный + стандартный отчёт за 10.05.2025 (Суббота).";

        // Act
        var result = reportBuilder.AddBodyReportText(cars, date, true).GetReport();

        // Assert
        Assert.StartsWith(expectedTitle, result);
    }

    [Fact]
    public void AddBodyReportText_WithoutAddTitle_NoTitleInReport()
    {
        // Arrange
        var reportBuilder = new GasAndDefaultReportBuilder();
        var cars = new List<Car>
        {
            new Car { Number = "111", IsWorked = true }
        };
        var date = new DateTime(2025, 5, 10);

        // Act
        var result = reportBuilder.AddBodyReportText(cars, date, false).GetReport();

        // Assert
        Assert.DoesNotContain("Топливный + стандартный отчёт за", result);
    }

    [Fact]
    public void AddBodyReportText_NonWorkingCar_OnlyDefaultReportPart()
    {
        // Arrange
        var reportBuilder = new GasAndDefaultReportBuilder();
        var cars = new List<Car>
        {
            new Car { Number = "111", IsWorked = false }
        };
        var date = new DateTime(2025, 5, 10);

        // Act
        var result = reportBuilder.AddBodyReportText(cars, date, false).GetReport();

        // Assert
        Assert.Contains("111 не работал.", result);
        Assert.DoesNotContain("заправка:", result);
    }

    [Fact]
    public void AddBodyReportText_CarWithScreen_HasScreenMark()
    {
        // Arrange
        var reportBuilder = new GasAndDefaultReportBuilder();
        var cars = new List<Car>
        {
            new Car { Number = "111", IsWorked = true, WasScreen = true }
        };
        var date = new DateTime(2025, 5, 10);

        // Act
        var result = reportBuilder.AddBodyReportText(cars, date, false).GetReport();

        // Assert
        Assert.Contains("111 (скрин), ок.", result);
    }

    [Fact]
    public void AddBodyReportText_CarWith24kmET_Has24kmETInfo()
    {
        // Arrange
        var reportBuilder = new GasAndDefaultReportBuilder();
        var cars = new List<Car>
        {
            new Car { Number = "111", IsWorked = true, Was24kmET = true }
        };
        var date = new DateTime(2025, 5, 10);

        // Act
        var result = reportBuilder.AddBodyReportText(cars, date, false).GetReport();

        // Assert
        Assert.Contains("111 ездил на 20 км енисейского тракта.", result);
    }

    [Fact]
    public void AddBodyReportText_CarWithParking_HasParkingInfo()
    {
        // Arrange
        var reportBuilder = new GasAndDefaultReportBuilder();
        var cars = new List<Car>
        {
            new Car { Number = "111", IsWorked = true, Parking = new List<int> { 10, 20 } }
        };
        var date = new DateTime(2025, 5, 10);

        // Act
        var result = reportBuilder.AddBodyReportText(cars, date, false).GetReport();

        // Assert
        Assert.Contains("Стоянки:", result);
        Assert.Contains("111 2 стоянки(10 мин, 20 мин).", result);
    }

    [Fact]
    public void AddBodyReportText_CarWithAddInfo_HasAddInfo()
    {
        // Arrange
        var reportBuilder = new GasAndDefaultReportBuilder();
        var cars = new List<Car>
        {
            new Car { Number = "111", IsWorked = true, AddInformation = new List<string> { "Замена колеса" } }
        };
        var date = new DateTime(2025, 5, 10);

        // Act
        var result = reportBuilder.AddBodyReportText(cars, date, false).GetReport();

        // Assert
        Assert.Contains("111 Замена колеса, ок.", result);
    }

    [Fact]
    public void AddBodyReportText_MultipleCars_CorrectOrder()
    {
        // Arrange
        var reportBuilder = new GasAndDefaultReportBuilder();
        var cars = new List<Car>
        {
            new Car { Number = "AAA", IsWorked = true, FuelBegin = 10, FuelEnd = 30 },
            new Car { Number = "BBB", IsWorked = true, FuelBegin = 20, FuelEnd = 50 }
        };
        var date = new DateTime(2025, 5, 10);

        // Act
        var result = reportBuilder.AddBodyReportText(cars, date, false).GetReport();

        // Assert - проверяем что оба отчёта есть и в правильном порядке
        Assert.Contains("AAA заправили с 10 до 30 литров(20), ок.", result);
        Assert.Contains("BBB заправили с 20 до 50 литров(30), ок.", result);
        Assert.Contains("AAA заправка: с 10 до 30 (20 литров).", result);
        Assert.Contains("BBB заправка: с 20 до 50 (30 литров).", result);
        Assert.Contains("Итого 50 литров.", result);
    }

    [Fact]
    public void GetReport_ReportTypeConstant_IsCorrect()
    {
        // Assert
        Assert.Equal("Топливный + стандартный отчёт", GasAndDefaultReportBuilder.ReportType);
    }

    [Fact]
    public void AddBodyReportText_MultipleCalls_ResetsPreviousState()
    {
        // Arrange
        var reportBuilder = new GasAndDefaultReportBuilder();
        var cars1 = new List<Car>
        {
            new Car { Number = "111", IsWorked = true }
        };
        var cars2 = new List<Car>
        {
            new Car { Number = "222", IsWorked = true }
        };
        var date = new DateTime(2025, 5, 10);

        // Act
        reportBuilder.AddBodyReportText(cars1, date, false);
        var result = reportBuilder.AddBodyReportText(cars2, date, false).GetReport();

        // Assert
        Assert.Contains("222 ок.", result);
        Assert.DoesNotContain("111", result);
    }
}
