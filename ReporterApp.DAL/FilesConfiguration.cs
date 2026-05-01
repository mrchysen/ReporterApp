using ReporterApp.Core;

namespace ReporterApp.DAL;

public static class FilesConfiguration
{
    private static string _dataFolder { get; set; } = "Data";

    private static string _carsNumbersFileName { get; set; } = "CarsNumbers.txt";

    private static string _windowConfigurationFileName { get; set; } = "Window.config.json";

    /// <summary>
    /// Путь к файлу с номерами машин
    /// </summary>
    public static string GetCarsNumbersFilePath 
        => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        AppConsts.ApplicationName,
                        _dataFolder, 
                        _carsNumbersFileName);

    /// <summary>
    /// Путь к папке с данными
    /// </summary>
    public static string GetDataFolderPath
        => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        AppConsts.ApplicationName,
                        _dataFolder);

    /// <summary>
    /// Путь к файлу с конфигурацией приложения
    /// </summary>
    public static string GetWindowConfigurationFilePath
        => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        AppConsts.ApplicationName,
                        _windowConfigurationFileName);

    /// <summary>
    /// Путь к папке с конфигурацией
    /// </summary>
    public static string GetConfigurationFolderPath
        => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        AppConsts.ApplicationName);
}
