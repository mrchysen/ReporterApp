using ReporterApp.Core.Cars;
using System.Text;

namespace ReporterApp.Core.Reports;

public class GasReportBuilder : BaseReportBuilder
{
    public const string ReportType = "Топливный отчёт";

    public GasReportBuilder() : base(null) { }

    public GasReportBuilder(ReportParams? reportParams = null) : base(reportParams) { }

    public override string GetReportType() => ReportType;

    public override BaseReportBuilder AddBodyReportText(List<Car> cars, bool addTitle = false)
    {
        if(addTitle)
            AddTitle();

        var fueledCars = cars.Where(c => CarUtils.IsCarWasFueled(c)).ToList();

        if (fueledCars.Count == 0)
        {
            return this;
        }

        StringBuilder sb = new();

        foreach (var car in fueledCars)
        {
            string text = car.FuelEnd > 0 && car.FuelBegin >= 0 ?
                $"заправка: с {car.FuelBegin} до {car.FuelEnd} ({car.FuelEnd - car.FuelBegin} литров)" :
                $"заправка: около {car.FuelBegin} литров";

            sb.AppendLine($"{car.Number} {text}.");
        }

        sb.AppendLine()
          .AppendLine($"Итого {fueledCars.Sum(c => Math.Abs(c.FuelBegin - c.FuelEnd)).ToString()} литров.");

        _report.Body = sb.ToString();

        return this;
    }

    public override BaseReportBuilder AddAdditionalText(string text)
    {
        _report.AdditionalInfo.Add(text);

        return this;
    }
}
