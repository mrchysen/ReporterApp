using ReporterApp.Core.Utils;

public class Car : ViewModelBase
{
    private string _number = string.Empty;
    private bool _isWorked;
    private int _fuelBegin;
    private int _fuelEnd;
    private bool _wasScreen;
    private bool _was24kmET;
    private List<int> _parking = new();
    private List<string> _addInformation = new();

    public string Number
    {
        get => _number;
        set
        {
            _number = value;
            NotifyPropertyChanged();
        }
    }

    public bool IsWorked
    {
        get => _isWorked;
        set
        {
            _isWorked = value;
            NotifyPropertyChanged();
        }
    }

    public int FuelBegin
    {
        get => _fuelBegin;
        set
        {
            _fuelBegin = value;
            NotifyPropertyChanged();
        }
    }

    public int FuelEnd
    {
        get => _fuelEnd;
        set
        {
            _fuelEnd = value;
            NotifyPropertyChanged();
        }
    }

    public bool WasScreen
    {
        get => _wasScreen;
        set
        {
            _wasScreen = value;
            NotifyPropertyChanged();
        }
    }

    public bool Was24kmET
    {
        get => _was24kmET;
        set
        {
            _was24kmET = value;
            NotifyPropertyChanged();
        }
    }

    public List<int> Parking
    {
        get => _parking;
        set
        {
            _parking = value;
            NotifyPropertyChanged();
        }
    }

    public List<string> AddInformation
    {
        get => _addInformation;
        set
        {
            _addInformation = value;
            NotifyPropertyChanged();
        }
    }
}