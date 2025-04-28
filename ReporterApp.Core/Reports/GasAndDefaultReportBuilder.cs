using ReporterApp.Core.Cars;

namespace ReporterApp.Core.Reports;

public class GasAndDefaultReportBuilder : BaseReportBuilder
{
    public const string ReportType = "Топливный + стандартный отчёт";

    public GasAndDefaultReportBuilder() : base(null) { }

    public GasAndDefaultReportBuilder(ReportParams? reportParams = null) : base(reportParams) { }

    public override string GetReportType() => ReportType;

    public override BaseReportBuilder AddBodyReportText(List<Car> cars, bool addTitle = false)
    {
        if(addTitle)
            AddTitle();

        var defaultBuilder = new DefaultReportBuilder(_reportParams).AddBodyReportText(cars, false);
        var gasReportBuilder = new GasReportBuilder(_reportParams).AddBodyReportText(cars, false);

        _report.Body = 
            defaultBuilder.Build().ToString() +
            gasReportBuilder.Build().ToString();

        return this;
    }

    public override BaseReportBuilder AddAdditionalText(string text)
    {
        _report.AdditionalInfo.Add(text);

        return this;
    }
}
