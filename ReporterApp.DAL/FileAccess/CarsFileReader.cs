using ReporterApp.Core.Cars;
using System.Text.Json;

namespace DAL.FileAccess;

public class CarsFileReader
{
    protected const string NotFoundFileText = "Info: Файла машин не нашлось(примерный файл <file_name>.car)";
    protected const string SuccessText = "Ok: Всё считалось";

    public CarsOperationInfo ReadJson(string path)
    {
        List<Car> cars = new List<Car>();

        if (!File.Exists(path))
            return GetInfoObject(cars, FileOperationResult.Info, NotFoundFileText);

        using StreamReader sr = new StreamReader(path);

        string json = sr.ReadToEnd();

        try
        {
            cars = JsonSerializer.Deserialize<List<Car>>(json) ?? new List<Car>();
        }
        catch (Exception ex)
        {
            return GetInfoObject(cars, FileOperationResult.Error, "Error: " + ex.Message);
        }

        return GetInfoObject(cars, FileOperationResult.Success, SuccessText);
    }

    public CarsOperationInfo ReadOnlyNumbers(string path)
    {
        List<Car> cars = new List<Car>();

        if (!File.Exists(path))
            return GetInfoObject(cars, FileOperationResult.Info, NotFoundFileText);

        try
        {
            using StreamReader reader = new StreamReader(path);

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
        catch (Exception ex)
        {
            return GetInfoObject(cars, FileOperationResult.Error, "Error: " + ex.Message);
        }

        if(cars.Count == 0)
        {
            return GetInfoObject(cars, FileOperationResult.Info, $"Info: в файле({path}) нет номеров машин.");
        }

        return GetInfoObject(cars, FileOperationResult.Success, SuccessText);
    }

    private CarsOperationInfo GetInfoObject(List<Car> cars, FileOperationResult result, string message) => new()
    {
        Cars = cars,
        Result = result,
        Message = message
    };
}