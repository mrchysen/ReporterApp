using ReporterApp.Core.Reports;

namespace ReporterApp.UnitTests.Core.Reports;

public class GasReportBuilderTests
{
    [Fact]
    public void AddBodyReportText_EmptyCarsList_EmptyReport()
    {
        // Arrange
        var reportBuilder = new GasReportBuilder();
        var cars = new List<Car>();
        var date = new DateTime(2025, 5, 10);

        // Act
        var result = reportBuilder.AddBodyReportText(cars, date, false).GetReport();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void AddBodyReportText_NoFueledCars_EmptyReport()
    {
        // Arrange
        var reportBuilder = new GasReportBuilder();
        var cars = new List<Car>
        {
            new Car { Number = "111", FuelBegin = 0, FuelEnd = 0 },
            new Car { Number = "222", FuelBegin = 10, FuelEnd = 10 },
            new Car { Number = "333", FuelBegin = 5, FuelEnd = 3 }
        };
        var date = new DateTime(2025, 5, 10);

        // Act
        var result = reportBuilder.AddBodyReportText(cars, date, false).GetReport();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void AddBodyReportText_WithFueledCars_CorrectReportText()
    {
        // Arrange
        var reportBuilder = new GasReportBuilder();
        var cars = new List<Car>
        {
            new Car { Number = "111", FuelBegin = 10, FuelEnd = 50 },
            new Car { Number = "222", FuelBegin = 20, FuelEnd = 60 }
        };
        var date = new DateTime(2025, 5, 10);

        var expected = """
            111 заправка: с 10 до 50 (40 литров).
            222 заправка: с 20 до 60 (40 литров).

            Итого 80 литров.
            """;

        // Act
        var result = reportBuilder.AddBodyReportText(cars, date, false).GetReport();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void AddBodyReportText_WithApproximateFueling_CorrectReportText()
    {
        // Arrange
        var reportBuilder = new GasReportBuilder();
        var cars = new List<Car>
        {
            new Car { Number = "111", FuelBegin = 30, FuelEnd = 0 }
        };
        var date = new DateTime(2025, 5, 10);

        var expected = """
            111 заправка: около 30 литров.

            Итого 30 литров.
            """;

        // Act
        var result = reportBuilder.AddBodyReportText(cars, date, false).GetReport();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void AddBodyReportText_WithMixedFueling_CorrectReportText()
    {
        // Arrange
        var reportBuilder = new GasReportBuilder();
        var cars = new List<Car>
        {
            new Car { Number = "111", FuelBegin = 10, FuelEnd = 40 },
            new Car { Number = "222", FuelBegin = 25, FuelEnd = 0 },
            new Car { Number = "333", FuelBegin = 0, FuelEnd = 0 } // не заправлялся
        };
        var date = new DateTime(2025, 5, 10);

        var expected = """
            111 заправка: с 10 до 40 (30 литров).
            222 заправка: около 25 литров.

            Итого 55 литров.
            """;

        // Act
        var result = reportBuilder.AddBodyReportText(cars, date, false).GetReport();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void AddBodyReportText_WithAddTitle_HasTitleInReport()
    {
        // Arrange
        var reportBuilder = new GasReportBuilder();
        var cars = new List<Car>
        {
            new Car { Number = "111", FuelBegin = 10, FuelEnd = 30 }
        };
        var date = new DateTime(2025, 5, 10); // Суббота

        var expected = """
            Топливный отчёт за 10.05.2025 (Суббота).

            111 заправка: с 10 до 30 (20 литров).

            Итого 20 литров.
            """;

        // Act
        var result = reportBuilder.AddBodyReportText(cars, date, true).GetReport();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void AddBodyReportText_MultipleCalls_ResetsPreviousState()
    {
        // Arrange
        var reportBuilder = new GasReportBuilder();
        var cars1 = new List<Car>
        {
            new Car { Number = "111", FuelBegin = 10, FuelEnd = 30 }
        };
        var cars2 = new List<Car>
        {
            new Car { Number = "222", FuelBegin = 20, FuelEnd = 50 }
        };
        var date = new DateTime(2025, 5, 10);

        var expected = """
            222 заправка: с 20 до 50 (30 литров).

            Итого 30 литров.
            """;

        // Act
        reportBuilder.AddBodyReportText(cars1, date, false);
        var result = reportBuilder.AddBodyReportText(cars2, date, false).GetReport();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetReport_ReportTypeConstant_IsCorrect()
    {
        // Assert
        Assert.Equal("Топливный отчёт", GasReportBuilder.ReportType);
    }

    [Theory]
    [InlineData(10, 20, 10)]
    [InlineData(5, 25, 20)]
    [InlineData(0, 50, 50)]
    public void AddBodyReportText_DifferentFuelAmounts_CorrectTotal(int begin, int end, int expectedTotal)
    {
        // Arrange
        var reportBuilder = new GasReportBuilder();
        var cars = new List<Car>
        {
            new Car { Number = "111", FuelBegin = begin, FuelEnd = end }
        };
        var date = new DateTime(2025, 5, 10);

        // Act
        var result = reportBuilder.AddBodyReportText(cars, date, false).GetReport();

        // Assert
        Assert.Contains($"Итого {expectedTotal} литров.", result);
    }
}
