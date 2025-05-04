using ReporterApp.WindowApp.Utils;
using System.Windows.Controls;

namespace ReporterApp.WindowApp.Pages.NewDesign.FileManagementPage;

public partial class FileManagementPage : Page
{
    public FileManagementPage(
        ViewModelMediator mediator)
    {
        InitializeComponent();

        DataContext = mediator.CreateFileManagementPageViewModel();
    }
}
