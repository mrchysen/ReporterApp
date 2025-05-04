using System.IO;
using WpfApp;

namespace Reporter.Configuration;

public static class FilesConfiguration
{
    private static string _dataFolder { get; set; } = "Data";

    private static string _carsNumbersFileName { get; set; } = "CarsNumbers.txt";

    private static string _windowConfigurationFileName { get; set; } = "Window.config.json";

    public static string GetCarsNumbersFilePath 
        => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        App.ApplicationName,
                        _dataFolder, 
                        _carsNumbersFileName);

    public static string GetDataFolderPath
        => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        App.ApplicationName,
                        _dataFolder);

    public static string GetWindowConfigurationFolderPath
        => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        App.ApplicationName,
                        _windowConfigurationFileName);
}
