using ReporterApp.Core.Cars;

namespace DAL.FileAccess;

public class CarsOperationInfo
{
    public FileOperationResult Result { get; set; }

    public List<Car>? Cars { get; set; }

    public string Message { get; set; } = string.Empty;
}
