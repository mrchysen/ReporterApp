using KrasTechMontacApplication.Logic.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrasTechMontacApplication.Logic.Reports;

public class GasReportBuilder : IReportBuilder
{
    protected Report Report = new();

    public string ReportType() => "Топливный отчёт";

    public Report Build() => Report;
    
    public IReportBuilder GetBaseReport(List<Car> cars)
    {
        StringBuilder sb = new StringBuilder();

        var GasCars = cars.Where(c => c.FuelEnd > 0 && c.FuelBegin >= 0 || c.FuelBegin > 0 && c.FuelEnd == 0).ToList();

        if(GasCars.Count > 0)
        {
            foreach (var car in GasCars) 
            {
                string text = "";

                if(car.FuelEnd > 0 && car.FuelBegin >= 0)
                {
                    text = $"заправка: с {car.FuelBegin} до {car.FuelEnd} ({car.FuelEnd - car.FuelBegin} литров)";
                }
                else // c.FuelBegin >= 0
                {
                    text = $"заправка: около {car.FuelBegin} литров";
                }

                sb.AppendLine($"{car.Number} {text}.");
            }

            sb.AppendLine();
            sb.AppendLine($"Итого {GasCars.Sum(c => Math.Abs(c.FuelBegin - c.FuelEnd)).ToString()} литров.");
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
