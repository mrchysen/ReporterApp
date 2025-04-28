using System.Windows;
using System.IO;
using System.Text.Json;
using Reporter.Logic.Configuration;
using System.Diagnostics;
using Microsoft.Win32;
using DAL.FileAccess;
using Reporter.Configuration;
using ReporterApp.Core.Managers;
using ReporterApp.Core.Reports;
using ReporterApp.WindowApp.Pages.NewDesign.StartPage;
using ReporterApp.WindowApp.Utils;

namespace WpfApp;

public partial class MainWindow : Window
{
    private PageNavigatorService _navigationService;
    
    // Manager keeps all data
    private AppManager _appManager;

    private WindowConfiguration _configuration = null!;
    private readonly FilesConfiguration _filesConfiguration = new();

    public MainWindow()
    {
        InitializeComponent();

        _navigationService = new(Frame);
        _navigationService.NavigateWithDataContext(new StartPage(new(_navigationService)));

        // Configuration \\
        ConfigureWindow();
        ConfigureFiles();

        var result = new CarsFileReader().ReadOnlyNumbers(_filesConfiguration.GetCarsNumbersFilePath);

        InfoObjectHandling(result);

        //_appManager = new(result.Cars == null? [new Car() { Number = "XXX "}] : result.Cars);

        // Cancel any menu events
        MenuFileButton.IsEnabled = false;
        CopyFileMenuButton.IsEnabled = false;
        SaveFileButton.IsEnabled = false;
    }

    private void ConfigureFiles()
    {
        Directory.CreateDirectory(_filesConfiguration.GetDataFolderPath);

        if(!File.Exists(_filesConfiguration.GetCarsNumbersFilePath)) 
            File.Create(_filesConfiguration.GetCarsNumbersFilePath);
    }

    #region Methods

    #region Start Configuration Methods
    protected void ConfigureWindow()
    {
        if (!File.Exists(_filesConfiguration.GetWindowConfigurationFolderPath))
        {
            SetDefaultWindowConfiguration();
            return;
        }

        string json = File.ReadAllText(_filesConfiguration.GetWindowConfigurationFolderPath);
        
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

        using var sw = new StreamWriter(_filesConfiguration.GetWindowConfigurationFolderPath, false);
            
        sw.WriteLine(json);

        sw.Close();
    }
    #endregion

    #region MenuItem - File - Buttons events
    private void OpenFileButton_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog fileDialog = new();
        fileDialog.InitialDirectory = _filesConfiguration.GetDataFolderPath;
        fileDialog.Filter = "(*.json)|*.json|All files (*.*)|*.*";

        if(fileDialog.ShowDialog() == true) 
        { 
            string filePath = fileDialog.FileName;
            
            var result = new CarsFileReader().ReadJson(filePath);

            InfoObjectHandling(result);

            _appManager.Cars = result.Cars;

            //PageHandler.Content = new CreatePage(this);
        }
    }
    private void CreateFileButton_Click(object sender, RoutedEventArgs e)
    {
        // code of creating new file with reports
        _appManager = new AppManager(_appManager);

        //PageHandler.Content = new CreatePage(this);
    }
    private void SaveFileButton_Click(object sender, RoutedEventArgs e)
    {
        // code that saves report/car files
        string directoryPath = Path.Combine(
            _filesConfiguration.GetDataFolderPath, 
            _appManager.ReportParams.Date.Year.ToString(), 
            _appManager.ReportParams.Date.Month.ToString());
        string path = Path.Combine(directoryPath, $"{_appManager.ReportParams.Date.ToShortDateString()}.car.json");

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

        var infoObject = new CarsFileWriter().WriteJson(_appManager.Cars!, path);

        InfoObjectHandling(infoObject);

        MessageBox.Show("Файл сохранён.", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
    }
    private void CopyFileButton_Click(object sender, RoutedEventArgs e)
    {
        // code of copying report into cache of PC
        Clipboard.SetText(_appManager.Builder.Build().ToString());
    }
    #endregion

    #region MenuItem - Menu - Buttons events
    private void CarNumberItem_Click(object sender, RoutedEventArgs e)
    {
        if (_appManager.Cars == null)
            return;

        //PageHandler.Content = new CarNumberPage(_appManager.Cars, _appManager);
    }

    private void FilePageItem_Click(object sender, RoutedEventArgs e)
    {
        //PageHandler.Content = new FileManagementPage(_appManager);
    }

    private void ReportPageItem_Click(object sender, RoutedEventArgs e)
    {
        //PageHandler.Content = new ReportPage(_appManager);
    }

    private void ResetCarButton_Click(object sender, RoutedEventArgs e)
    {
        var Result = MessageBox.Show("Очистить заданные данные в машины?", "Очистка", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (Result == MessageBoxResult.Yes)
        {
            var result = new CarsFileReader().ReadOnlyNumbers(_filesConfiguration.GetCarsNumbersFilePath);

            InfoObjectHandling(result);

            _appManager.Cars = result.Cars;

            // refresh view if it is ReportPage
            //if (//PageHandler.Content is ReportPage content)
            //{
            //    //PageHandler.Content = new ReportPage(_appManager);
            //}
        }
    }
    #endregion

    public void SetReportBuilder(BaseReportBuilder ReportBuilder)
    {
        _appManager = new AppManager(_appManager, ReportBuilder);
        SetAllEnable();

        //PageHandler.Content = new ReportPage(_appManager);
    }

    private void SetAllEnable()
    {
        MenuFileButton.IsEnabled = true;
        CopyFileMenuButton.IsEnabled = true;
        SaveFileButton.IsEnabled = true;
    }

    private void InfoObjectHandling(CarsOperationInfo result)
    {
        if (result.Result == FileOperationResult.Error ||
            result.Result == FileOperationResult.Info)
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

        //if (_appManager.CarsChanged)
        //{
        //    var carWriter = new CarsFileWriter();

        //    var result = carWriter.WriteCarNumbers(_appManager.Cars ?? [], _filesConfiguration.GetCarsNumbersFilePath);

        //    InfoObjectHandling(result);
        //}
    }

    #endregion

    #endregion
}