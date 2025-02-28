using Core.Reports;
using System.Windows;
using System.Windows.Controls;

namespace ReporterApp.WindowApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreatePage.xaml
    /// </summary>
    public partial class CreatePage : Page
    {
        protected MainWindow MainWindow;

        public CreatePage(MainWindow mainWindow)
        {
            InitializeComponent();
            FillWithReportTypes();

            MainWindow = mainWindow;
        }

        protected void FillWithReportTypes()
        { 
            StackOfReportsType.Children.Add(CreateButton<DefaultReportBuilder>("Стандартный отчёт"));
            StackOfReportsType.Children.Add(CreateButton<GasReportBuilder>("Топливный отчёт"));
            StackOfReportsType.Children.Add(CreateButton<GasAndDefaultReportBuilder>("Стандартный + топливный\n\t отчёт"));
        }

        protected Button CreateButton<T>(string text) where T : IReportBuilder , new()
        {
            Button button = new ()
            {
                Width = 200,
                Margin = new Thickness(5, 0, 0, 0),
                Content = text,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
            };

            button.Click += (o, e) =>
            {
                TypeChosen(new T());
            };

            return button;
        }

        protected void TypeChosen(IReportBuilder reportBuilder)
        {
            MainWindow.SetReportBuilder(reportBuilder);
        }
    }
}
