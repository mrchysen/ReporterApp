using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace ReporterApp.WindowApp.Configuration;

public class MainWindowConfigurationService
{
    private string _launchUrl = string.Empty;

    public WindowConfiguration ConfigureWindow(Window window)
    {
        var configuration = GetConfigurationFromFile();

        window.WindowState = configuration!.State;
        window.Left = configuration.Location.X;
        window.Top = configuration.Location.Y;
        window.Width = configuration.Size.Width;
        window.Height = configuration.Size.Height;

        return configuration;
    }

    public void SaveWindowConfiguration(Window window)
    {
        var configuration = GetCurrentConfiguration(window);

        string json = JsonSerializer.Serialize(configuration);

        using var sw = new StreamWriter(
            FilesConfiguration.GetWindowConfigurationFilePath, 
            false);

        sw.WriteLine(json);

        sw.Close();
    }

    private WindowConfiguration GetCurrentConfiguration(Window window)
    {
        var configuration = new WindowConfiguration();

        configuration.State = window.WindowState;
        configuration.Location = new(window.Left, window.Top);
        configuration.Size = new(window.Width, window.Height);
        configuration.UrlOfWebToLaunch = _launchUrl;

        return configuration;
    }

    public WindowConfiguration GetConfigurationFromFile()
    {
        var pathToConfiguration = 
            FilesConfiguration.GetWindowConfigurationFilePath;

        if (!File.Exists(pathToConfiguration))
            return new WindowConfiguration();

        WindowConfiguration? resultConfiguration = null;

        try
        {
            var json = File.ReadAllText(
                pathToConfiguration);

            Debug.WriteLine("Файл конфигурации окна прочитан: \n" + json);

            resultConfiguration = 
                JsonSerializer.Deserialize<WindowConfiguration>(json);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Ошибка: " + ex.Message);
        }

        resultConfiguration = resultConfiguration ?? new WindowConfiguration();

        _launchUrl = resultConfiguration.UrlOfWebToLaunch;

        return resultConfiguration;
    }
}
