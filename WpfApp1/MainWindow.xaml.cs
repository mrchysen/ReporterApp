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
using DAL.FileAccess;
using Core.Cars;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Manager keeps all data
        private FileManager _manager;

        private WindowConfiguration _configuration;

        public MainWindow()
        {
            InitializeComponent();

            // Configuration \\
            ConfigureWindow();

            // Default manager. It is empty for default
            _manager = new();

            var result = new CarReader().ReadOnlyNumbers(Directory.GetCurrentDirectory() + "\\cars.txt");

            InfoObjectHandling(result);

            _manager.Cars = result.Cars;

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
                _configuration = JsonSerializer.Deserialize<WindowConfiguration>(json) 
                    ?? SetDefaultWindowConfiguration();

                this.WindowState = _configuration.State;
                this.Left = _configuration.Location.X;
                this.Top = _configuration.Location.Y;
                this.Width = _configuration.Size.Width;
                this.Height = _configuration.Size.Height;

                if (string.IsNullOrEmpty(_configuration.PathOfCarsFolder))
                {
                    _configuration.PathOfCarsFolder = Path.Combine(Directory.GetCurrentDirectory(), "CarsFolder");
                }
            }
            catch (Exception ex) 
            {
                ErrorHandling("Ошибка: " + ex.Message);

                SetDefaultWindowConfiguration();
            }
        }

        protected WindowConfiguration SetDefaultWindowConfiguration()
        {
            this.WindowState = WindowState.Normal;
            this.Left = 100;
            this.Top = 100;
            this.Width = this.MinWidth;
            this.Height = this.MinHeight;

            _configuration = new((int)MinWidth, (int)MinHeight);

            return _configuration;
        }

        protected WindowConfiguration GetCurrentConfiguration()
        {
            _configuration.State = WindowState;
            _configuration.Location = new(this.Left, this.Top);
            _configuration.Size = new(this.Width, this.Height);

            return _configuration;
        }

        protected void SaveWindowConfiguration()
        {
            _configuration = GetCurrentConfiguration();

            string json = JsonSerializer.Serialize(_configuration);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "window.config.json");

            using StreamWriter sw = new StreamWriter(path, false);
                sw.WriteLine(json);

            sw.Close();
        }

        #endregion

        #region MenuItem - File - Buttons events
        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            // code of openning file with report
            OpenFileDialog fileDiag = new();
            fileDiag.InitialDirectory = _configuration.PathOfCarsFolder;
            fileDiag.Filter = "(*.json)|*.json|All files (*.*)|*.*";

            if(fileDiag.ShowDialog() == true) 
            { 
                string filePath = fileDiag.FileName;
                
                var result = new CarReader().ReadJson(filePath);

                InfoObjectHandling(result);

                _manager.Cars = result.Cars;

                PageHandler.Content = new CreatePage(this);
            }
        }
        private void CreateFileButton_Click(object sender, RoutedEventArgs e)
        {
            // code of creating new file with reports
            _manager = new FileManager(_manager);

            PageHandler.Content = new CreatePage(this);
        }
        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            // code that saves report/car files
            string directoryPath = Path.Combine(
                _configuration.PathOfCarsFolder, 
                _manager.Date.Year.ToString(), 
                _manager.Date.Month.ToString());
            string path = Path.Combine(directoryPath, $"{_manager.Date.ToShortDateString()}.car.json");

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

            var infoObject = new CarWriter().WriteJson(_manager.Cars!, path);

            InfoObjectHandling(infoObject);

            MessageBox.Show("Файл сохранён.", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void CopyFileButton_Click(object sender, RoutedEventArgs e)
        {
            // code of copying report into cache of PC
            Clipboard.SetText(_manager.Builder.Build().ToString());
        }
        #endregion

        #region MenuItem - Setting - Buttons events
        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            PageHandler.Content = new SettingsPage(_configuration);
        }
        #endregion

        #region MenuItem - Menu - Buttons events
        private void CarNumberItem_Click(object sender, RoutedEventArgs e)
        {
            if (_manager.Cars == null)
                return;

            PageHandler.Content = new CarNumberPage(_manager.Cars, _configuration);
        }

        private void FilePageItem_Click(object sender, RoutedEventArgs e)
        {
            PageHandler.Content = new FileManagementPage(_manager);
        }

        private void ReportPageItem_Click(object sender, RoutedEventArgs e)
        {
            PageHandler.Content = new ReportPage(_manager);
        }

        private void ResetCarButton_Click(object sender, RoutedEventArgs e)
        {
            var Result = MessageBox.Show("Очистить заданные данные в машины?", "Очистка", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (Result == MessageBoxResult.Yes)
            {
                var result = new CarReader().ReadOnlyNumbers(Directory.GetCurrentDirectory() + "\\cars.txt");

                InfoObjectHandling(result);

                _manager.Cars = result.Cars;

                // refresh view if it is ReportPage
                if (PageHandler.Content is ReportPage content)
                {
                    PageHandler.Content = new ReportPage(_manager);
                }
            }
        }
        #endregion

        public void SetReportBuilder(IReportBuilder ReportBuilder)
        {
            _manager = new FileManager(_manager, ReportBuilder);
            SetAllEnable();

            PageHandler.Content = new ReportPage(_manager);
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

            if (_configuration.CarsChanged)
            {
                CarWriter carWriter = new CarWriter();

                string path = Directory.GetCurrentDirectory() + "\\cars.txt";

                carWriter.WriteCarNumbers(_manager.Cars ?? new List<Car>(), path);
            }
        }

        #endregion

        #endregion
    }
}