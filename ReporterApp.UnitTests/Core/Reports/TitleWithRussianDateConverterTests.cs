using ReporterApp.Core.Reports.Utils;

namespace ReporterApp.UnitTests.Core.Reports;

/// <summary>
/// Тесты для конвертера дат в русский формат.
/// </summary>
public class TitleWithRussianDateConverterTests
{
    [Theory]
    [InlineData(DayOfWeek.Monday, "Понедельник")]
    [InlineData(DayOfWeek.Tuesday, "Вторник")]
    [InlineData(DayOfWeek.Wednesday, "Среда")]
    [InlineData(DayOfWeek.Thursday, "Четверг")]
    [InlineData(DayOfWeek.Friday, "Пятница")]
    [InlineData(DayOfWeek.Saturday, "Суббота")]
    [InlineData(DayOfWeek.Sunday, "Воскресенье")]
    public void GetTitle_AllDaysOfWeek_CorrectRussianTranslation(DayOfWeek dayOfWeek, string expectedRussianName)
    {
        // Arrange
        var date = new DateTime(2025, 1, 6); // Понедельник
        // Находим дату с нужным днём недели
        var dateWithCorrectDay = date.AddDays((int)dayOfWeek - (int)date.DayOfWeek);
        var reportType = "Стандартный отчёт";

        // Act
        var result = TitleWithRussianDateConverter.GetTitle(reportType, dateWithCorrectDay);

        // Assert
        Assert.Contains(expectedRussianName, result);
    }

    [Fact]
    public void GetTitle_ValidDate_CorrectFormat()
    {
        // Arrange
        var date = new DateTime(2025, 5, 10);
        var reportType = "Стандартный отчёт";

        // Act
        var result = TitleWithRussianDateConverter.GetTitle(reportType, date);

        // Assert
        Assert.Equal("Стандартный отчёт за 10.05.2025 (Суббота).", result);
    }

    [Fact]
    public void GetTitle_DifferentReportType_CorrectReportTypeInTitle()
    {
        // Arrange
        var date = new DateTime(2025, 5, 10);
        var reportType = "Топливный отчёт";

        // Act
        var result = TitleWithRussianDateConverter.GetTitle(reportType, date);

        // Assert
        Assert.StartsWith("Топливный отчёт за", result);
    }

    [Fact]
    public void GetTitle_EmptyReportType_EmptyReportTypeInTitle()
    {
        // Arrange
        var date = new DateTime(2025, 5, 10);
        var reportType = string.Empty;

        // Act
        var result = TitleWithRussianDateConverter.GetTitle(reportType, date);

        // Assert
        Assert.Equal(" за 10.05.2025 (Суббота).", result);
    }

    [Theory]
    [InlineData(1, 1, "01.01")]
    [InlineData(12, 31, "31.12")]
    [InlineData(6, 15, "15.06")]
    public void GetTitle_DifferentDates_CorrectDateFormat(int month, int day, string expectedDatePart)
    {
        // Arrange
        var date = new DateTime(2025, month, day);
        var reportType = "Отчёт";

        // Act
        var result = TitleWithRussianDateConverter.GetTitle(reportType, date);

        // Assert
        Assert.Contains(expectedDatePart, result);
    }

    [Fact]
    public void GetTitle_Year2000_CorrectYear()
    {
        // Arrange
        var date = new DateTime(2000, 1, 1);
        var reportType = "Отчёт";

        // Act
        var result = TitleWithRussianDateConverter.GetTitle(reportType, date);

        // Assert
        Assert.Contains("2000", result);
    }

    [Fact]
    public void GetTitle_FutureDate_CorrectFutureDate()
    {
        // Arrange
        var date = new DateTime(2030, 6, 15);
        var reportType = "Отчёт";

        // Act
        var result = TitleWithRussianDateConverter.GetTitle(reportType, date);

        // Assert
        Assert.Contains("2030", result);
    }
}
