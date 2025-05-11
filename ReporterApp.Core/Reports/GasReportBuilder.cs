using ReporterApp.Core.Cars;
using ReporterApp.Core.Reports.Utils;
using System.Text;

namespace ReporterApp.Core.Reports;

public class GasReportBuilder : IReportBuilder
{
    private StringBuilder _titleSb = new();
    private StringBuilder _bodySb = new();

    private bool _isTitleAdded = false;

    public const string ReportType = "Топливный отчёт";

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

        var fueledCars = cars.Where(c => CarUtils.IsCarWasFueled(c)).ToList();

        if (fueledCars.Count == 0)
        {
            return this;
        }

        foreach (var car in fueledCars)
        {
            string text = car.FuelEnd > 0 && car.FuelBegin >= 0 ?
                $"заправка: с {car.FuelBegin} до {car.FuelEnd} ({car.FuelEnd - car.FuelBegin} литров)" :
                $"заправка: около {car.FuelBegin} литров";

            _bodySb.AppendLine($"{car.Number} {text}.");
        }

        _bodySb.AppendLine()
          .AppendLine($"Итого {fueledCars.Sum(c => Math.Abs(c.FuelBegin - c.FuelEnd)).ToString()} литров.");

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
