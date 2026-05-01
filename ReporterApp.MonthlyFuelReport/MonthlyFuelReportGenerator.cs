using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;

namespace ReporterApp.MonthlyFuelReport;

public class MonthlyFuelReportGenerator
{
    public void GenerateReport(List<CarMonthFuelInfo> cars, string outputPath)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Monthly Fuel Report");

        worksheet.Cell(1, 1).Value = "Номер машины";
        worksheet.Cell(1, 2).Value = "Дата";
        worksheet.Cell(1, 3).Value = "Количество литров";

        var headerRange = worksheet.Range(1, 1, 1, 3);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

        int row = 2;

        foreach (var car in cars)
        {
            int offset = 0;

            worksheet.Cell(row, 1).Value = car.Number;

            foreach (var fuelInfo in car.FuelInfos) 
            {
                worksheet.Cell(row + offset, 2).Value = fuelInfo.Date;
                worksheet.Cell(row + offset, 3).Value = fuelInfo.FuelDif;

                offset++;
            }

            worksheet.Cell(row + offset , 1).Value = $"Итого по {car.Number}";
            worksheet.Cell(row + offset, 3).Value = car.FuelInfos.Sum(fi => fi.FuelDif);

            int totalRow = row + offset;

            var carRange = worksheet.Range(row, 1, totalRow, 3);
            carRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            carRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            worksheet.Range(totalRow, 1, totalRow, 3).Style.Font.Bold = true;

            row += car.FuelInfos.Count + 1; // + 1 - это для строки итогов
        }

        worksheet.Cell(row, 1).Value = $"Общий итог";
        worksheet.Cell(row, 3).Value = cars.Sum(fi => fi.FuelInfos.Sum(fi => fi.FuelDif));

        worksheet.Columns().AdjustToContents();
        worksheet.Column(2).Width = worksheet.Column(2).Width + 2;

        workbook.SaveAs(outputPath);
    }
}