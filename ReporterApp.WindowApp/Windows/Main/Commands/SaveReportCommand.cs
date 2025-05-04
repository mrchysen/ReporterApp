using DAL.FileAccess;
using Reporter.Configuration;
using ReporterApp.Core.Managers;
using ReporterApp.WindowApp.Utils;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace ReporterApp.WindowApp.Windows.Main.Comands;

public class SaveReportCommand : BaseMainWindowCommand
{
    private readonly ViewModelMediator _mediator;

    public SaveReportCommand(ViewModelMediator mediator)
    {
        _mediator = mediator;
    }

    public override bool CanExecute(object? parameter)
        => _mediator.ReportPageViewModel.Builder != null;

    public override void Execute(object? parameter)
    {
        // code that saves car files
        var date = _mediator.FileManagementPageViewModel.ReportDate;

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

        var cars = _mediator.ReportPageViewModel.Cars;

        var infoObject = new CarsFileWriter().WriteJson(cars, path);
        Debug.WriteLine(infoObject);

        Clipboard.SetText(_mediator.ReportPageViewModel.ReportText);

        MessageBox.Show(
            "Файл сохранён.", 
            "Информация", 
            MessageBoxButton.OK, 
            MessageBoxImage.Warning);
    }
}
