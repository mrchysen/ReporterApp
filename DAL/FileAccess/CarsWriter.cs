using Core.Cars;

namespace DAL.FileAccess;

public class CarsWriter
{
    public FileOperationResult Write(List<Car> cars, string pathOfDirectory, string fileName)
    {
        var mainFilePath = Path.Combine(pathOfDirectory, fileName);

        Directory.CreateDirectory(pathOfDirectory);


        var carsWithImgs = cars.Where(c => c.WasScreen);
        var carWriter = new CarsFileWriter();

        var mainFileInfo = carWriter.WriteJson(cars, mainFilePath);
        var pistureFilesInfo = carWriter.WriteImages(cars, pathOfDirectory);

        return FileOperationResult.Info;
    }
}
