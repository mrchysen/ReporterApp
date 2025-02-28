namespace Core.Cars;

public class Car
{
    private MemoryStream? _memoryStream;

    public string Number { get; set; } = string.Empty;
    public bool IsWorked { get; set; } = false;
    public int FuelBegin { get; set; } = 0;
    public int FuelEnd { get; set; } = 0;
    public string ImgPath { get; set; } = string.Empty;
    public bool Was24kmET { get; set; } = false;
    public List<int> Parking { get; set; } = new List<int>();
    public List<string> AddInformation { get; set; } = new List<string>();
    public bool WasScreen => !String.IsNullOrEmpty(ImgPath) && _memoryStream != null;
    public MemoryStream PictureMemoryStream 
    {
        get
        {
            var result = _memoryStream ?? new MemoryStream();

            _memoryStream = result;
            
            return result;
        }
    }

    public override string ToString() => Number;
}
