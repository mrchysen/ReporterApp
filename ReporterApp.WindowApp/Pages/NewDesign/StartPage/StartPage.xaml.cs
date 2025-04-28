using ReporterApp.WindowApp.Utils;
using System.Windows.Controls;

namespace ReporterApp.WindowApp.Pages.NewDesign.StartPage;

public record StartPageParams(PageNavigatorService PageNavigatorService);

public partial class StartPage : Page
{
    public StartPage(StartPageParams @params)
    {
        InitializeComponent();

        DataContext = new StartPageViewModel(@params.PageNavigatorService);
    }
}
