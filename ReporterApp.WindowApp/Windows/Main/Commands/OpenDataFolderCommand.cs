using Reporter.Configuration;
using ReporterApp.WindowApp.Windows.Main.Comands;
using System.Diagnostics;
using System.IO;

namespace ReporterApp.WindowApp.Windows.Main.Commands;

public class OpenDataFolderCommand : BaseMainWindowCommand
{
    private Process? _currentProcess;

    public override void Execute(object? parameter)
    {
        try
        {
            if (!Directory.Exists(FilesConfiguration.GetDataFolderPath))
            {
                Debug.WriteLine("Папка не существует!");

                return;
            }

            _currentProcess?.Kill();

            _currentProcess = Process.Start(new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = $"\"{FilesConfiguration.GetDataFolderPath}\"",
                UseShellExecute = true,
                Verb = "open"
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Ошибка при открытии папки: {ex.Message}");
        }
    }
}
