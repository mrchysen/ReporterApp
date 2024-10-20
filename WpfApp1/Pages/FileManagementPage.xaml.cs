using Core.Managers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;


namespace Reporter.Pages
{
    /// <summary>
    /// Логика взаимодействия для FileManagementPage.xaml
    /// </summary>
    public partial class FileManagementPage : Page
    {
        protected FileManager FileManager;

        public FileManagementPage(FileManager fileManager)
        {
            InitializeComponent();

            FileManager = fileManager;

            DatePicker.SelectedDate = fileManager.Date;
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
}
