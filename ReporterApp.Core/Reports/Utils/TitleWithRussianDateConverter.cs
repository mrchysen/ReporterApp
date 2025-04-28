namespace ReporterApp.Core.Reports.Utils;

public static class TitleWithRussianDateConverter
{
    public static string GetTitle(string currentReportBuilderType, DateTime dateTime) 
        => $"{currentReportBuilderType} за {dateTime.ToShortDateString()} " +
            $"({TranslateDateName(dateTime.DayOfWeek)}).";

    private static string TranslateDateName(DayOfWeek day)
    {
        return day switch
        {
            DayOfWeek.Monday => "Понедельник",
            DayOfWeek.Tuesday => "Вторник",
            DayOfWeek.Wednesday => "Среда",
            DayOfWeek.Thursday => "Четверг",
            DayOfWeek.Friday => "Пятница",
            DayOfWeek.Saturday => "Суббота",
            DayOfWeek.Sunday => "Воскресенье",
            _ => "Не день"
        };
    }
}
