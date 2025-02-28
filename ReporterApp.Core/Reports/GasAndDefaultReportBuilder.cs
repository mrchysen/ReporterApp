using Core.Cars;

namespace Core.Reports;

public class GasAndDefaultReportBuilder : IReportBuilder
{
    protected Report Report = new();
    public string ReportType() => "Топливный + стандартный отчёт";

    public Report Build() => Report;

    public IReportBuilder GetBaseReport(List<Car> cars)
    {
        IReportBuilder DefaultBuilder = new DefaultReportBuilder().GetBaseReport(cars);
        IReportBuilder GasReportBuilder = new GasReportBuilder().GetBaseReport(cars);

        Report.BasePart = DefaultBuilder.Build().BasePart + "\n" + GasReportBuilder.Build().BasePart;

        return this;
    }

    public IReportBuilder GetTitle(string text)
    {
        Report.Title = text;

        return this;
    }
}
