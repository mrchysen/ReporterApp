using DAL.FileAccess;
using ReporterApp.Core.Cars;
using ReporterApp.DAL;
using ReporterApp.MonthlyFuelReport;

var pathToFolder = FilesConfiguration.GetDataFolderPath + "\\2026\\3";
var fileInfo = new DirectoryInfo(pathToFolder).GetFiles("*.json");

var carsFileReader = new CarsFileReader();
var cars = new List<CarWithDate>();

foreach (var file in fileInfo)
{
    var date = DateFromPathParser.ExtractDateFromCarFile(file);
    var result = carsFileReader.ReadJson(file.FullName);

    if (result.Result == FileOperationResult.Success)
    {
        cars.AddRange(
            result.Cars.Select(c =>
                CarWithDate.Map(c, date)));
    }
    else
    {
        Console.WriteLine($"Ошибка чтения файла {file.Name}: {result.Message}");
    }
}

Console.WriteLine($"Загружено машин: {cars.Count}");

var groupedCars = cars
    .Where(CarUtils.IsCarWasFueled)
    .Cast<CarWithDate>()
    .GroupBy(c => c.Number)
    .Select(c =>
    new CarMonthFuelInfo(
        c.Key,
    c.Select(cwd => new FuelInfo(cwd.Date, cwd.FuelBegin, cwd.FuelEnd))
              .ToList()
    ))
    .ToList();

var excelReportGenerator = new MonthlyFuelReportGenerator();
excelReportGenerator.GenerateReport(groupedCars, ".\\MonthlyFuelReport.xlsx");

Console.WriteLine("Отчет сгенерирован успешно!");

