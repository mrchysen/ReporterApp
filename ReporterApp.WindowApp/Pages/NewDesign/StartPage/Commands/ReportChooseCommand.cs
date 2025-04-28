using ReporterApp.Core.Reports;
using ReporterApp.WindowApp.Utils;
using System.Windows;
using System.Windows.Input;

namespace ReporterApp.WindowApp.Pages.NewDesign.StartPage.Commands;

public class ReportChooseCommand<T> : ICommand where T : BaseReportBuilder, new()
{
    private PageNavigatorService _navigationServie;

    public ReportChooseCommand(PageNavigatorService navigationServie)
    {
        _navigationServie = navigationServie;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
        => _navigationServie.NavigateWithDataContext(new ReportPage.ReportPage(new T()));
    
}
