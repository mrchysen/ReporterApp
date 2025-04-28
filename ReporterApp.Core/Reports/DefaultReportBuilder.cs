using ReporterApp.Core.Cars;
using ReporterApp.Core.Reports.Utils;
using System.Text;

namespace ReporterApp.Core.Reports;

public class DefaultReportBuilder : BaseReportBuilder
{
    public const string ReportType = "Стандартный отчёт";

    public DefaultReportBuilder() : base(null) { }

    public DefaultReportBuilder(ReportParams? reportParams = null) : base(reportParams) { }

    public override string GetReportType() => ReportType;

    public override BaseReportBuilder AddBodyReportText(List<Car> cars, bool addTitle = false)
    {
        if(addTitle)
            AddTitle();

        StringBuilder sb = new();

        foreach(Car car in cars.Where(c => c.IsWorked)) 
        {
            var carSb = new StringBuilder();

            if (car.WasScreen)
            {
                carSb.Append("(скрин), ");
            }

            if(car.FuelEnd == 0 && car.FuelBegin > 0)
            {
                carSb.Append($"была заправка(около {car.FuelBegin}), ");
            }
            else if(car.FuelEnd > 0 && car.FuelBegin > 0)
            {
                carSb.Append($"заправили с {car.FuelBegin} до {car.FuelEnd} литров({car.FuelEnd - car.FuelBegin}), ");
            }

            if(car.AddInformation.Count > 0)
            {
                carSb.Append(string.Join(", ", car.AddInformation));
                carSb.Append(", ");
            }

            carSb.Append("ок.");
            
            sb.AppendLine($"{car.Number} {carSb.ToString()}");
        }

        var cars24kmNumbers = cars
            .Where(c => c.Was24kmET && c.IsWorked)
            .Select(c => c.Number)
            .ToList();

        if (cars24kmNumbers.Count > 0)
        {
            sb.AppendLine()
              .Append(string.Join(", ", cars24kmNumbers))
              .AppendLine(cars24kmNumbers.Count == 1 ? 
              " ездил на 20 км енисейского тракта." : 
              " ездили на 20 км енисейского тракта.");
        }

        var parkingCars = cars
            .Where(c => c.Parking.Count > 0 && c.IsWorked)
            .Select(c => new { c.Number, c.Parking })
            .ToList();

        if (parkingCars.Count > 0)
        {
            Func<int, string> parkingWord = new Func<int, string>((i) =>
            {
                if (i == 1)
                    return "стоянка";
                else if (1 < i && i <= 4)
                    return "стоянки";
                return "стоянок";
            });

            sb.AppendLine()
              .AppendLine("Стоянки:")
              .AppendLine(string.Join('\n', parkingCars.Select(c => 
              $"{c.Number} {c.Parking.Count} {parkingWord(c.Parking.Count)}({string.Join(" мин, ", c.Parking)} мин).")));
        }

        _report.Body = sb.ToString();

        return this;
    }

    public override BaseReportBuilder AddAdditionalText(string text)
    {
        _report.AdditionalInfo.Add(text);

        return this;
    }
}