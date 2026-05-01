namespace ReporterApp.MonthlyFuelReport;

public class CarWithDate : Car
{
    public DateTime Date { get; set; }

    public static CarWithDate Map(Car car, DateTime dateTime)
    {
        ArgumentNullException.ThrowIfNull(car);

        var carWithDate = new CarWithDate();

        car.CloneTo(carWithDate);

        carWithDate.Date = dateTime;

        return carWithDate;
    }
}
