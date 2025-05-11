using DAL.FileAccess;
using Microsoft.Win32;
using Reporter.Configuration;
using ReporterApp.Core.Reports;
using ReporterApp.WindowApp.Pages.NewDesign;
using ReporterApp.WindowApp.Pages.NewDesign.StartPage;
using ReporterApp.WindowApp.Utils;
using System.Diagnostics;

namespace ReporterApp.WindowApp.Windows.Main.Comands;

public class OpenReportCommand : BaseMainWindowCommand
{
    private ViewModelMediator _mediator;
    private PageNavigatorService _pageNavigatorService;

    public OpenReportCommand(
        ViewModelMediator mediator, 
        PageNavigatorService pageNavigatorService)
    {
        _mediator = mediator;
        _pageNavigatorService = pageNavigatorService;
    }

    public override void Execute(object? parameter)
    {
        OpenFileDialog fileDialog = new();
        fileDialog.InitialDirectory = FilesConfiguration.GetDataFolderPath;
        fileDialog.Filter = "(*.json)|*.json|All files (*.*)|*.*";

        if (fileDialog.ShowDialog() == true)
        {
            string filePath = fileDialog.FileName;

            var result = new CarsFileReader().ReadJson(filePath);

            Debug.WriteLine(result);

            _mediator.SetCars(result.Cars);
            _mediator.SetOpenReportStatus(needToReadCar: false);
            _mediator.SetDate(ParseDateFromFilePath(filePath));

            _pageNavigatorService.NavigateTo(
                new StartPage(_mediator));
        }
    }

    public DateTime ParseDateFromFilePath(string filePath)
    {
        var subStr = filePath.Replace(".car.json", "");
        var index = subStr.LastIndexOf("\\");
        var dateStr = subStr.Substring(index).Replace("\\", "");

        return DateTime.Parse(dateStr);
    }
}
