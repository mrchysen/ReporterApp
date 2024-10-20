using System.Text.Json;

namespace Core.Cars.FileAccess;

public class CarWriter
{
    public InfoObject WriteJson(List<Car> cars, string path)
    {
        try
        {
            using StreamWriter sw = new StreamWriter(path, false);

            string json = JsonSerializer.Serialize(cars);

            sw.Write(json);
        }
        catch (Exception ex)
        {
            return new()
            {
                Cars = cars,
                Result = FileResult.Error,
                Message = "Error: " + ex.Message
            };
        }

        return new()
        {
            Cars = cars,
            Result = FileResult.Success,
            Message = "Ok: Файл записан"
        };
    }
}
