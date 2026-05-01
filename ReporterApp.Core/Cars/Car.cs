using CommunityToolkit.Mvvm.ComponentModel;

public partial class Car : ObservableObject
{
    [ObservableProperty]
    private string _number = string.Empty;

    [ObservableProperty]
    private bool _isWorked;

    [ObservableProperty]
    private int _fuelBegin;

    [ObservableProperty]
    private int _fuelEnd;

    [ObservableProperty]
    private bool _wasScreen;

    [ObservableProperty]
    private bool _was24kmET;

    [ObservableProperty]
    private List<int> _parking = new();

    [ObservableProperty]
    private List<string> _addInformation = new();

    public Car CloneTo(Car car)
    {
        car.Number = Number;
        car.IsWorked = IsWorked;
        car.FuelBegin = FuelBegin;
        car.FuelEnd = FuelEnd;
        car.WasScreen = WasScreen;
        car.Was24kmET = Was24kmET;
        car.Parking = [.. Parking];
        car.AddInformation = [.. AddInformation];

        return car;
    }
}