using Reporter.Configuration;
using Reporter.Logic.Configuration;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace ReporterApp.WindowApp.Configuration;

public class MainWindowConfigurationService
{
    private WindowConfiguration _windowConfiguration = new();

    public void ConfigureWindow(Window window)
    {
        var pathToConfiguration = FilesConfiguration.GetWindowConfigurationFolderPath;

        if (!File.Exists(pathToConfiguration))
        {
            SetDefaultWindowConfiguration(window);
            return;
        }

        string json = File.ReadAllText(FilesConfiguration.GetWindowConfigurationFolderPath);

        Debug.WriteLine("Файл конфигурации окна прочитан: \n" + json);

        try
        {
            var configuration = JsonSerializer.Deserialize<WindowConfiguration>(json);

            if(configuration == null)
            {
                SetDefaultWindowConfiguration(window);
            }

            window.WindowState = configuration!.State;
            window.Left = configuration.Location.X;
            window.Top = configuration.Location.Y;
            window.Width = configuration.Size.Width;
            window.Height = configuration.Size.Height;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Ошибка: " + ex.Message);

            SetDefaultWindowConfiguration(window);
        }
    }

    private void SetDefaultWindowConfiguration(Window window)
    {
        window.WindowState = WindowState.Normal;
        window.Left = 100;
        window.Top = 100;
        window.Width = window.MinWidth;
        window.Height = window.MinHeight;
    }

    private WindowConfiguration GetCurrentConfiguration(Window window)
    {
        var configuration = new WindowConfiguration();

        configuration.State = window.WindowState;
        configuration.Location = new(window.Left, window.Top);
        configuration.Size = new(window.Width, window.Height);

        return configuration;
    }

    public void SaveWindowConfiguration(Window window)
    {
        var configuration = GetCurrentConfiguration(window);

        string json = JsonSerializer.Serialize(configuration);

        using var sw = new StreamWriter(FilesConfiguration.GetWindowConfigurationFolderPath, false);

        sw.WriteLine(json);

        sw.Close();
    }
}
