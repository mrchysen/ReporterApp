namespace Core.Cars;

public class Car
{
    public string Number { get; set; } = string.Empty;
    public bool IsWorked { get; set; } = false;
    public int FuelBegin { get; set; } = 0;
    public int FuelEnd { get; set; } = 0;
    public bool WasScreen { get; set; } = false;
    public bool Was24kmET { get; set; } = false;
    public List<int> Parking { get; set; } = new List<int>();
    public List<string> AddInformation { get; set; } = new List<string>();
    
    public override string ToString()
    {
        return Number;
    }
}
