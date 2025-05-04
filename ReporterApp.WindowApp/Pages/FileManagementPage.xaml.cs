using ReporterApp.Core.Managers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Reporter.Pages;

public partial class FileManagementPage : Page
{
    protected AppManager FileManager;

    public FileManagementPage(AppManager fileManager)
    {
        InitializeComponent();

        FileManager = fileManager;

        DatePicker.SelectedDate = DateTime.Now;
        DatePicker.LayoutTransform = new ScaleTransform(1.25,1.25);
        SetBindings();
    }

    protected void SetBindings()
    {
        // DatePicker - SelectedDate \\
        Binding DatePickerBinding = new Binding("Date");
        DatePickerBinding.Source = FileManager;
        DatePickerBinding.Mode = BindingMode.TwoWay;
        DatePicker.SetBinding(DatePicker.SelectedDateProperty, DatePickerBinding);
    }
}
