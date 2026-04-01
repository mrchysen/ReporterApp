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
        car.Number = _number;
        car.IsWorked = _isWorked;
        car.FuelBegin = _fuelBegin;
        car.FuelEnd = _fuelEnd;
        car.WasScreen = _wasScreen;
        car.Was24kmET = _was24kmET;
        car.Parking = [.. _parking];
        car.AddInformation = [.. _addInformation];

        return car;
    }
}