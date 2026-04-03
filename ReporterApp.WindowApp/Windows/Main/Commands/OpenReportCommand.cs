using ReporterApp.DAL.FileAccess;
using Microsoft.Win32;
using ReporterApp.WindowApp.Configuration;
using ReporterApp.WindowApp.Navigation;
using ReporterApp.WindowApp.Services;
using System.Diagnostics;

namespace ReporterApp.WindowApp.Windows.Main.Commands;

public class OpenReportCommand : BaseMainWindowCommand
{
    private readonly ICarService _carService;
    private readonly IFileManagementService _fileManagementService;
    private readonly INavigationService _navigationService;

    public OpenReportCommand(
        ICarService carService,
        IFileManagementService fileManagementService,
        INavigationService navigationService)
    {
        _carService = carService;
        _fileManagementService = fileManagementService;
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        OpenFileDialog fileDialog = new()
        {
            InitialDirectory = FilesConfiguration.GetDataFolderPath,
            Filter = "(*.json)|*.json|All files (*.*)|*.*"
        };

        if (fileDialog.ShowDialog() == true)
        {
            string filePath = fileDialog.FileName;

            var result = new CarsFileReader().ReadJson(filePath);

            Debug.WriteLine(result);

            _carService.SetCarsWithReset(result.Cars);
            _fileManagementService.ReportDate = ParseDateFromFilePath(filePath);

            _navigationService.NavigateTo<Pages.NewDesign.StartPage.StartPage>(false);
        }
    }

    private DateTime ParseDateFromFilePath(string filePath)
    {
        var subStr = filePath.Replace(".car.json", "");
        var index = subStr.LastIndexOf("\\");
        var dateStr = subStr.Substring(index).Replace("\\", "");

        return DateTime.Parse(dateStr);
    }
}
