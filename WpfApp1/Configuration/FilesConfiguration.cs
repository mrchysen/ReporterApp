using System.IO;
using WpfApp1;

namespace Reporter.Configuration;

public class FilesConfiguration
{
    private string _dataFolder { get; set; } = "Data";

    private string _carsNumbersFileName { get; set; } = "CarsNumbers.txt";

    private string _windowConfigurationFileName { get; set; } = "Window.config.json";

    public string GetCarsNumbersFilePath 
        => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        App.ApplicationName,
                        _dataFolder, 
                        _carsNumbersFileName);

    public string GetDataFolderPath
        => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        App.ApplicationName,
                        _dataFolder);

    public string GetWindowConfigurationFolderPath
        => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        App.ApplicationName,
                        _windowConfigurationFileName);
}
