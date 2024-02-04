using System.Text.Json;
using System.IO;
using System.Windows;


namespace KrasTechMontacApplication.Logic.Cars;

public class CarReader
{
    public static List<Car> ReadJson(string path) 
    { 
        List<Car> cars = new List<Car>();

        if(File.Exists(path)) 
        { 
            using(StreamReader sr = new StreamReader(path)) 
            { 
                string json = sr.ReadToEnd();
                try
                {
                    cars = JsonSerializer.Deserialize<List<Car>>(json) ?? new List<Car>();
                }
                catch (Exception e) 
                {
                    MessageBox.Show($"Error: {e.Message}",
                                     "Error",
                                     MessageBoxButton.OK,
                                     MessageBoxImage.Error);
                }
            }
        }
        else
        {
            MessageBox.Show($"Info: Файла машин не нашлось(примерный файл <file_name>.car)",
                             "Info",
                             MessageBoxButton.OK,
                             MessageBoxImage.Information);
        }

        return cars;
    }

    public static List<Car> ReadOnlyNumbers(string path)
    {
        List<Car> cars = new List<Car>();

        if (File.Exists(path))
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    var line = string.Empty;
                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        if (!string.IsNullOrEmpty(line))
                        {
                            cars.Add(new Car { Number = line });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error: {e.Message}",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
        else
        {
            MessageBox.Show($"Info: Файла машин не нашлось(файл cars.txt)",
                             "Info",
                             MessageBoxButton.OK,
                             MessageBoxImage.Information);
        }

        return cars;
    }
}

public class CarWriter
{
    public static bool WriteJson(string path, List<Car> cars)
    {
        bool WasWritten = false;

        try
        {
            using(StreamWriter sw = new StreamWriter(path, false)) 
            { 
                string json = JsonSerializer.Serialize<List<Car>>(cars);

                sw.Write(json);

                WasWritten = true;
            }
        }
        catch(Exception e) 
        {
            WasWritten = false;
            MessageBox.Show($"Error: {e.Message}",
                             "Error",
                             MessageBoxButton.OK,
                             MessageBoxImage.Error);
        }

        return WasWritten;
    }
}
