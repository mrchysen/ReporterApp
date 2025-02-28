using Core.Cars;

namespace Core.Reports;

public interface IReportBuilder
{
    string ReportType();
    IReportBuilder GetTitle(string text);
    IReportBuilder GetBaseReport(List<Car> cars);
    Report Build();

}
