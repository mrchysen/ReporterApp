using ReporterApp.DAL.FileAccess;

namespace ReporterApp.WindowApp.Services;

/// <summary>
/// Сервис для управления данными автомобилей
/// </summary>
public interface ICarService
{
    /// <summary>
    /// Все автомобили
    /// </summary>
    List<Car> Cars { get; set; }

    /// <summary>
    /// Загрузить автомобили из файла
    /// </summary>
    void LoadCars(string filePath);

    /// <summary>
    /// Сохранить автомобили в файл
    /// </summary>
    void SaveCars(string filePath);

    /// <summary>
    /// Установить автомобили и сбросить enumerator
    /// </summary>
    void SetCarsWithReset(List<Car> cars);
}

/// <summary>
/// Реализация сервиса управления автомобилями
/// </summary>
public class CarService : ICarService
{
    private readonly ICarsFileWriter _carsFileWriter;

    public CarService(ICarsFileWriter? carsFileWriter = null)
    {
        _carsFileWriter = carsFileWriter ?? new CarsFileWriter();
        Cars = [];
    }

    public List<Car> Cars { get; set; }

    public void LoadCars(string filePath)
    {
        var result = new CarsFileReader()
            .ReadOnlyNumbers(filePath);

        Cars = result.Cars ?? [];
    }

    public void SaveCars(string filePath)
    {
        _carsFileWriter.WriteCarNumbers(Cars, filePath);
    }

    public void SetCarsWithReset(List<Car> cars)
    {
        Cars = cars;
    }
}
