using ReporterApp.Core.Cars;
using ReporterApp.Core.Reports.Utils;

namespace ReporterApp.Core.Reports;

public abstract class BaseReportBuilder
{
    protected readonly Report _report = new();
    protected ReportParams _reportParams;

    public BaseReportBuilder(ReportParams? reportParams = null)
    {
        _reportParams = reportParams ?? new();
    }

    public ReportParams ReportParams
    {
        get => _reportParams;
        set => _reportParams = value;
    }

    public abstract string GetReportType();

    protected virtual BaseReportBuilder AddTitle(string? text = null) 
    {
        _report.Title = TitleWithRussianDateConverter.GetTitle(
            text ?? GetReportType(),
            _reportParams.Date);

        return this;
    }

    public abstract BaseReportBuilder AddBodyReportText(List<Car> cars, bool addTitle = false);

    public abstract BaseReportBuilder AddAdditionalText(string text);

    public virtual Report Build() => _report;
}
