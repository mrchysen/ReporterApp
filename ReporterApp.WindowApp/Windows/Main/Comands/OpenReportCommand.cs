using DAL.FileAccess;
using Microsoft.Win32;
using Reporter.Configuration;
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

            var builder = _mediator.GetReportPageViewModel()!.Builder;

            _mediator.CreateReportPageViewModel(builder, result.Cars);

            _pageNavigatorService.NavigateTo(
                new StartPage(_mediator, _pageNavigatorService));
        }
    }
}
