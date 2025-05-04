namespace ReporterApp.Core.Cars;

public class CarUtils
{
    public static Func<Car, bool> IsCarWasFueled = (c) =>
        c.FuelEnd > 0 && c.FuelBegin >= 0 ||
        c.FuelBegin > 0 && c.FuelEnd == 0;
}
