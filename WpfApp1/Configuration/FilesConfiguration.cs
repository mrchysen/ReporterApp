using System.IO;
using WpfApp1;

namespace Reporter.Configuration;

public class FilesConfiguration
{
    private string DataFolder { get; set; } = "Data";

    private string CarsNumbersFileName { get; set; } = "CarsNumbers.txt";

    public string GetCarsNumbersFilePath 
        => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        App.ApplicationName,
                        DataFolder, 
                        CarsNumbersFileName);

    public string GetCarsNumbersFolderPath
        => GetDataFolderPath;

    public string GetDataFolderPath
        => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        App.ApplicationName,
                        DataFolder);
}
