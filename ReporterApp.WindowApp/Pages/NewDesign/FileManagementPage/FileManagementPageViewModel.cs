using CommunityToolkit.Mvvm.ComponentModel;

namespace ReporterApp.WindowApp.Pages.NewDesign.FileManagementPage;

public partial class FileManagementPageViewModel : ObservableObject
{
    [ObservableProperty]
    private DateTime _reportDate = DateTime.Now.AddDays(-1);
}
