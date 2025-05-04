namespace ReporterApp.Core.Reports;

public interface IReportBuilder
{
    IReportBuilder AddBodyReportText(
        List<Car> cars,
        DateTime date,
        bool addTitle);

    string GetReport();
}