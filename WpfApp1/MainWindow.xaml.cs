using System.Windows;
using WpfApp1.Pages;
using System.IO;
using System.Text.Json;
using Reporter.Logic.Configuration;
using System.Diagnostics;
using Reporter.Pages;
using Microsoft.Win32;
using Core.Managers;
using Core.Reports;
using Core.Cars.FileAccess;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Manager keeps all data
        private FileManager Manager;
        private WindowConfiguration Configuration;

        public MainWindow()
        {
            InitializeComponent();

            // Configuration \\
            ConfigureWindow();

            // Default manager. It is empty for default
            Manager = new();

            var result = new CarReader().ReadOnlyNumbers(Directory.GetCurrentDirectory() + "\\cars.txt");

            InfoObjectHandling(result);

            Manager.Cars = result.Cars;

            // First page - default page
            PageHandler.Content = new CreatePage(this);
            
            // Cancel any menu events
            MenuFileButton.IsEnabled = false;
            CopyFileMenuButton.IsEnabled = false;
            SaveFileButton.IsEnabled = false;
        }

        #region Methods

        #region Start Configuration Methods
        protected void ConfigureWindow()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "window.config.json");

            if (!File.Exists(path))
            {
                SetDefaultWindowConfiguration();
                return;
            }

            string json = File.ReadAllText(path);
            
            Debug.WriteLine("Нашёл файл");
            try
            {
                Configuration = JsonSerializer.Deserialize<WindowConfiguration>(json);

                this.WindowState = Configuration.State;
                this.Left = Configuration.Location.X;
                this.Top = Configuration.Location.Y;
                this.Width = Configuration.Size.Width;
                this.Height = Configuration.Size.Height;

                if (string.IsNullOrEmpty(Configuration.PathOfCarsFolder))
                {
                    Configuration.PathOfCarsFolder = Path.Combine(Directory.GetCurrentDirectory(), "CarsFolder");
                }
            }
            catch (Exception ex) 
            {
                ErrorHandling("Ошибка: " + ex.Message);

                SetDefaultWindowConfiguration();
            }
        }

        protected void SetDefaultWindowConfiguration()
        {
            this.WindowState = WindowState.Normal;
            this.Left = 100;
            this.Top = 100;
            this.Width = this.MinWidth;
            this.Height = this.MinHeight;

            Configuration = new((int)MinWidth, (int)MinHeight);
        }

        protected void SaveWindowConfiguration()
        {
            string json = JsonSerializer.Serialize(Configuration);

            using StreamWriter sw = new StreamWriter(string.Concat(Directory.GetCurrentDirectory(), "\\window.config.json"), false);
                sw.WriteLine(json);
        }

        #endregion

        #region MenuItem - File - Buttons events
        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            // code of openning file with report
            OpenFileDialog fileDiag = new();
            fileDiag.InitialDirectory = Configuration.PathOfCarsFolder;
            fileDiag.Filter = "(*.json)|*.json|All files (*.*)|*.*";

            if(fileDiag.ShowDialog() == true) 
            { 
                string filePath = fileDiag.FileName;
                
                var result = new CarReader().ReadJson(filePath);

                InfoObjectHandling(result);

                Manager.Cars = result.Cars;

                PageHandler.Content = new CreatePage(this);
            }
        }
        private void CreateFileButton_Click(object sender, RoutedEventArgs e)
        {
            // code of creating new file with reports
            Manager = new FileManager(Manager);

            PageHandler.Content = new CreatePage(this);
        }
        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            // code that saves report/car files
            string directoryPath = Path.Combine(
                Configuration.PathOfCarsFolder, 
                Manager.Date.Year.ToString(), 
                Manager.Date.Month.ToString());
            string path = Path.Combine(directoryPath, $"{Manager.Date.ToShortDateString()}.car.json");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (File.Exists(path))
            {
                var result = MessageBox.Show("Такой файл уже существует. Перезаписать его?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            
                if(result == MessageBoxResult.No) 
                {
                    return;
                }
            }

            var infoObject = new CarWriter().WriteJson(Manager.Cars!, path);

            InfoObjectHandling(infoObject);

            MessageBox.Show("Файл сохранён.", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void CopyFileButton_Click(object sender, RoutedEventArgs e)
        {
            // code of copying report into cache of PC
            Clipboard.SetText(Manager.Builder.Build().ToString());
        }
        #endregion

        #region MenuItem - Setting - Buttons events
        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            PageHandler.Content = new SettingsPage(Configuration);
        }
        #endregion

        #region MenuItem - Menu - Buttons events
        private void FilePageItem_Click(object sender, RoutedEventArgs e)
        {
            PageHandler.Content = new FileManagementPage(Manager);
        }

        private void ReportPageItem_Click(object sender, RoutedEventArgs e)
        {
            PageHandler.Content = new ReportPage(Manager);
        }

        private void ResetCarButton_Click(object sender, RoutedEventArgs e)
        {
            var Result = MessageBox.Show("Очистить заданные данные в машины?", "Очистка", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (Result == MessageBoxResult.Yes)
            {
                var result = new CarReader().ReadOnlyNumbers(Directory.GetCurrentDirectory() + "\\cars.txt");

                InfoObjectHandling(result);

                Manager.Cars = result.Cars;

                // refresh view if it is ReportPage
                if (PageHandler.Content is ReportPage content)
                {
                    PageHandler.Content = new ReportPage(Manager);
                }
            }
        }
        #endregion

        public void SetReportBuilder(IReportBuilder ReportBuilder)
        {
            Manager = new FileManager(Manager, ReportBuilder);
            SetAllEnable();

            PageHandler.Content = new ReportPage(Manager);
        }

        private void SetAllEnable()
        {
            MenuFileButton.IsEnabled = true;
            CopyFileMenuButton.IsEnabled = true;
            SaveFileButton.IsEnabled = true;
        }

        private void InfoObjectHandling(InfoObject result)
        {
            if (result.Result == FileResult.Error ||
                result.Result == FileResult.Info)
            {
                MessageBox.Show(
                    result.Message,
                    result.Result.ToString(),
                    MessageBoxButton.OK,
                    MessageBoxImage.None);
            }
        }

        private void ErrorHandling(string message) => MessageBox.Show(
                    message,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.None);

        #region Window events

        private void Window_Closed(object sender, EventArgs e)
        {
            SaveWindowConfiguration();
        }

        #endregion

        #endregion
    }
}