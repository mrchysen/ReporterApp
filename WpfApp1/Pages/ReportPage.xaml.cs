using KrasTechMontacApplication.Logic.FileClasses;
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
        protected FileManager _Manager;
        public ReportPage(FileManager manager)
        {
            InitializeComponent();
            Manager = manager;
            SetBindings();
        }

        protected void SetBindings()
        {
            // TextBox - Text\\
            Binding TextBoxBinding = new Binding();
            TextBoxBinding.Source = _Manager.Iterator;
            TextBoxBinding.Path = new PropertyPath("GetCars");
            TextBoxBinding.Converter = new ReportConverter(_Manager.Builder, _Manager.Date);
            TextBoxBinding.Mode = BindingMode.OneWay;
            TextBox.SetBinding(TextBox.TextProperty, TextBoxBinding);
        }

        protected FileManager Manager { set { _Manager = value; CarIteratorComponent.Iterator = _Manager.Iterator; } }
    }
}
