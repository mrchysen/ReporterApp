using KrasTechMontacApplication.Logic.Cars;
using KrasTechMontacApplication.Logic.Reports;
using System.Globalization;
using System.Windows.Data;

namespace Reporter.Logic.ValueConverters;

public class ReportConverter : IValueConverter
{
    protected IReportBuilder? ReportBuilder;
    protected DateTime Date;

    public ReportConverter(IReportBuilder reportBuilder, DateTime date)
    {
        ReportBuilder = reportBuilder;
        Date = date;
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        ReportBuilder.GetTitle($"{ReportBuilder.ReportType()} за {Date.ToShortDateString()} ({TranslateDateName(Date.DayOfWeek)}).");
        
        ReportBuilder.GetBaseReport((List<Car>)value);

        return ReportBuilder.Build().ToString();
    }

    protected string TranslateDateName(DayOfWeek day)
    {
        return day switch
        {
            DayOfWeek.Monday => "Понедельник",
            DayOfWeek.Tuesday => "Вторник",
            DayOfWeek.Wednesday => "Среда",
            DayOfWeek.Thursday => "Четверг",
            DayOfWeek.Friday => "Пятница",
            DayOfWeek.Saturday => "Суббота",
            DayOfWeek.Sunday => "Воскресенье"
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
