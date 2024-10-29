using Core.Cars;
using System.Text.Json;

namespace DAL.FileAccess;

public class CarReader
{
    protected const string NotFoundFileText = "Info: Файла машин не нашлось(примерный файл <file_name>.car)";
    protected const string SuccessText = "Ok: Всё считалось";

    public InfoObject ReadJson(string path)
    {
        List<Car> cars = new List<Car>();

        if (!File.Exists(path))
            return GetInfoObject(cars, FileResult.Info, NotFoundFileText);

        using StreamReader sr = new StreamReader(path);

        string json = sr.ReadToEnd();

        try
        {
            cars = JsonSerializer.Deserialize<List<Car>>(json) ?? new List<Car>();
        }
        catch (Exception ex)
        {
            return GetInfoObject(cars, FileResult.Error, "Error: " + ex.Message);
        }

        return GetInfoObject(cars, FileResult.Success, SuccessText);
    }

    public InfoObject ReadOnlyNumbers(string path)
    {
        List<Car> cars = new List<Car>();

        if (!File.Exists(path))
            return GetInfoObject(cars, FileResult.Info, NotFoundFileText);

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
            return GetInfoObject(cars, FileResult.Error, "Error: " + ex.Message);
        }

        return GetInfoObject(cars, FileResult.Success, SuccessText);
    }

    private InfoObject GetInfoObject(List<Car> cars, FileResult result, string message) => new()
    {
        Cars = cars,
        Result = result,
        Message = message
    };
}