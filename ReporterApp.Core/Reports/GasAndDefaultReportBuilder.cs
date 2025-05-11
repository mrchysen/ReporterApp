using ReporterApp.Core.Reports.Utils;
using System.Text;

namespace ReporterApp.Core.Reports;

public class GasAndDefaultReportBuilder : IReportBuilder
{
    private StringBuilder _titleSb = new();
    private StringBuilder _bodySb = new();

    private bool _isTitleAdded = false;

    public const string ReportType = "Топливный + стандартный отчёт";

    public IReportBuilder AddBodyReportText(
        List<Car> cars, 
        DateTime date,
        bool addTitle = false)
    {
        _titleSb = new();
        _bodySb = new();

        if (addTitle)
        {
            AddTitle(date);

            _isTitleAdded = true;
        }

        var defaultReport = new DefaultReportBuilder()
            .AddBodyReportText(cars, date).GetReport();
        var gasReport = new GasReportBuilder()
            .AddBodyReportText(cars, date).GetReport();

        _bodySb.Append(defaultReport)
            .Append('\n')
            .Append(gasReport);

        return this;
    }

    public string GetReport()
    {
        var sb = _isTitleAdded ?
            _titleSb.Append('\n').Append(_bodySb) :
            _bodySb;

        return sb.RemoveAllCr().EnsureLastLfRemoved().ToString();
    }

    private void AddTitle(DateTime date)
    {
        _titleSb.AppendLine(TitleWithRussianDateConverter.GetTitle(
            ReportType,
            date));
    }
}
