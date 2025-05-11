using ReporterApp.Core.Reports;

namespace ReporterApp.UnitTests.Core.Reports;

public class DefaultReportBuilderTests
{
    [Fact]
    public void AddBodyReportText_CreateReport_Success()
    {
        // Arrange
        var reportBuilder = new DefaultReportBuilder();

        List<Car> cars = [
            new Car(){ Number = "111", IsWorked = true},
            new Car(){ Number = "222", IsWorked = true, WasScreen = true}
            ];

        var date = new DateTime(2025, 5, 10);
        
        var expectedReportText = 
            """
            Стандартный отчёт за 10.05.2025 (Суббота).

            111 ок.
            222 (скрин), ок.
            """;

        // Act
        var resultReportText = reportBuilder.AddBodyReportText(cars, date, true).GetReport();

        // Assert
        Assert.Equal(expectedReportText, resultReportText);
    }
}
