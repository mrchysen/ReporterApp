using KrasTechMontacApplication.Logic.Cars;
using System.Text;


namespace KrasTechMontacApplication.Logic.Reports;

public class StandartReportBuilder : IReportBuilder
{
    protected Report Report = new();

    public string ReportType() => "Стандартный отчёт";

    public Report Build()
    {
        return Report;
    }

    public IReportBuilder GetBaseReport(List<Car> cars)
    {
        StringBuilder sb = new StringBuilder();
        
        foreach(Car car in cars) 
        { 
            var Number = car.Number;

            StringBuilder BasePart = new StringBuilder("не работал.");

            if (car.IsWorked)
            {
                BasePart = new StringBuilder();

                if (car.WasScreen)
                {
                    BasePart.Append("(скрин), ");
                }

                if(car.FuelEnd == 0 && car.FuelBegin > 0)
                {
                    BasePart.Append($"была заправка(около {car.FuelBegin}), ");
                }
                else if(car.FuelEnd > 0 && car.FuelBegin > 0)
                {
                    BasePart.Append($"заправили с {car.FuelBegin} до {car.FuelEnd} литров({car.FuelEnd - car.FuelBegin}), ");
                }

                if(car.AddInformation.Count > 0)
                {
                    BasePart.Append(string.Join(", ", car.AddInformation));
                    BasePart.Append(", ");
                }

                BasePart.Append("ок.");
            }

            sb.AppendLine($"{Number} {BasePart}");
        }

        if(cars.Any((c) => c.Was24kmET && c.IsWorked))
        {
            sb.AppendLine();

            var cars24ET = cars.Where(c => c.Was24kmET && c.IsWorked).Select(c => c.Number).ToList();

            sb.Append(string.Join(", ", cars24ET))
              .AppendLine((cars24ET.Count == 1) ? (" ездил на 20 км енисейского тракта.") : (" ездили на 20 км енисейского тракта."));
        }

        if(cars.Any(c => c.Parking.Count > 0 && c.IsWorked))
        {
            sb.AppendLine();
            sb.AppendLine("Стоянки:");

            var carsParking = cars.Where(c => c.Parking.Count > 0 && c.IsWorked).Select(c => new { c.Number, c.Parking }).ToList();

            Func<int,string> parkingWord = new Func<int,string>((int i) =>
            {
                if (i == 1)
                    return "стоянка";
                else if(1 < i && i <= 4)
                    return "стоянки";
                return "стоянок";
            });

            foreach(var element in carsParking) 
            {
                sb.AppendLine($"{element.Number} {element.Parking.Count} {parkingWord(element.Parking.Count)}({string.Join(" мин, ", element.Parking)} мин).");
            }
        }

        Report.BasePart = sb.ToString();

        return this;
    }



    public IReportBuilder GetTitle(string text)
    {
        Report.Title = text;

        return this;
    }
}
