using Core.Managers;
using Reporter.Logic.ValueConverters;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfApp1.Elements;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        private FileManager _manager;

        public ReportPage(FileManager manager)
        {
            InitializeComponent();
            _manager = manager;
            SetBindings();
        }

        protected void SetBindings()
        {
            // TextBox - Text\\
            Binding TextBoxBinding = new Binding();
            TextBoxBinding.Source = _manager.Iterator;
            TextBoxBinding.Path = new PropertyPath("GetCars");
            TextBoxBinding.Converter = new ReportConverter(_manager.Builder, _manager.Date);
            TextBoxBinding.Mode = BindingMode.OneWay;
            TextBox.SetBinding(TextBox.TextProperty, TextBoxBinding);
        }

        protected FileManager Manager { set { _manager = value; CarIteratorComponent.Iterator = _manager.Iterator; } }
    }
}
