using Core.Cars;

namespace DAL.FileAccess;

public class InfoObject
{
    public FileResult Result { get; set; }

    public List<Car>? Cars { get; set; }

    public string Message { get; set; } = string.Empty;
}
