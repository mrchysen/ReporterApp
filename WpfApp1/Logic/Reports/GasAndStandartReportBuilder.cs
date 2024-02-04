using KrasTechMontacApplication.Logic.Cars;

namespace KrasTechMontacApplication.Logic.Reports;

public class GasAndStandartReportBuilder : IReportBuilder
{
    protected Report Report = new();
    public string ReportType() => "Топливный + стандартный отчёт";

    public Report Build() => Report;

    public IReportBuilder GetBaseReport(List<Car> cars)
    {
        IReportBuilder Standartbuilder = new StandartReportBuilder().GetBaseReport(cars);
        IReportBuilder GasReportBuilder = new GasReportBuilder().GetBaseReport(cars);

        Report.BasePart = Standartbuilder.Build().BasePart + "\n" + GasReportBuilder.Build().BasePart;

        return this;
    }

    public IReportBuilder GetTitle(string text)
    {
        Report.Title = text;

        return this;
    }
}
