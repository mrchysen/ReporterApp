using ReporterApp.DAL.FileAccess;
using ReporterApp.WindowApp.Configuration;
using ReporterApp.WindowApp.Services;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace ReporterApp.WindowApp.Windows.Main.Commands;

public class SaveReportCommand : BaseMainWindowCommand
{
    private readonly IReportService _reportService;
    private readonly ICarService _carService;
    private readonly IFileManagementService _fileManagementService;

    public SaveReportCommand(
        IReportService reportService,
        ICarService carService,
        IFileManagementService fileManagementService)
    {
        _reportService = reportService;
        _carService = carService;
        _fileManagementService = fileManagementService;
    }

    public override bool CanExecute(object? parameter)
        => _reportService.Builder != null;

    public override void Execute(object? parameter)
    {
        var date = _fileManagementService.ReportDate;

        string directoryPath = Path.Combine(
            FilesConfiguration.GetDataFolderPath,
            date.Year.ToString(),
            date.Month.ToString());

        string path = Path.Combine(directoryPath, $"{date.ToShortDateString()}.car.json");

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        if (File.Exists(path))
        {
            var result = MessageBox.Show("Такой файл уже существует. Перезаписать его?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.No)
            {
                return;
            }
        }

        var cars = _carService.Cars;

        var infoObject = new CarsFileWriter().WriteJson(cars, path);
        Debug.WriteLine(infoObject);

        Clipboard.SetText(_reportService.Builder!.GetReport());
    }
}
