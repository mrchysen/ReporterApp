using Core.Cars;
using System.Text.Json;

namespace DAL.FileAccess;

public class CarsFileWriter
{
    public CarsOperationInfo WriteImages(List<Car> carsWithImages, string fileFolder)
    {
        foreach(var car in carsWithImages)
        {
            using (var sr = new FileStream(Path.Combine(fileFolder, car.ImgPath), FileMode.Create))
            {
                car.PictureMemoryStream.CopyTo(sr);

                sr.Close();
            }
        }

        return new CarsOperationInfo()
        {
            Message = "Все изображения сохранены",
            Result = FileOperationResult.Success
        };
    }

    public CarsOperationInfo WriteJson(List<Car> cars, string path)
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
                Result = FileOperationResult.Error,
                Message = "Error: " + ex.Message
            };
        }

        return new()
        {
            Cars = cars,
            Result = FileOperationResult.Success,
            Message = "Ok: Файл записан"
        };
    }

    public CarsOperationInfo WriteCarNumbers(List<Car> cars, string path)
    {
        var textWithNumbers = string.Join("\r\n", cars.Select(c => c.Number));

        try
        {
            using StreamWriter sw = new(path, false);

            sw.Write(textWithNumbers);

            sw.Close();
        }
        catch (Exception ex)
        {
            return new()
            {
                Cars = cars,
                Result = FileOperationResult.Error,
                Message = "Error: " + ex.Message
            };
        }

        return new()
        {
            Cars = cars,
            Result = FileOperationResult.Success,
            Message = "Ok: Файл записан"
        };
    }
}