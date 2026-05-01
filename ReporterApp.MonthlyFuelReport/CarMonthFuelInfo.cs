namespace ReporterApp.MonthlyFuelReport;

public record CarMonthFuelInfo(string Number, List<FuelInfo> FuelInfos);

public record FuelInfo(DateTime Date, int FuelBegin, int FuelEnd)
{
    public int FuelDif => FuelEnd - FuelBegin;
}
