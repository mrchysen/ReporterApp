using System.Windows.Controls;

namespace ReporterApp.WindowApp.Pages.NewDesign.FileManagementPage;

public partial class FileManagementPage : Page
{
    public FileManagementPage(FileManagementPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
