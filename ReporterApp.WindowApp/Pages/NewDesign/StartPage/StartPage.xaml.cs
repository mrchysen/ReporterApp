using ReporterApp.WindowApp.Utils;
using System.Windows.Controls;

namespace ReporterApp.WindowApp.Pages.NewDesign.StartPage;

public partial class StartPage : Page
{
    public StartPage(ViewModelMediator Mediator)
    {
        InitializeComponent();

        DataContext = Mediator.StartPageViewModel;
    }
}
