using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Windows;

namespace Reporter.Logic.Configuration;

public class WindowConfiguration : INotifyPropertyChanged
{
    protected string _PathOfCarsFolder = Directory.GetCurrentDirectory() + "\\CarsFolder";

    public WindowConfiguration() { }

    public WindowConfiguration(int minWidth, int minHeight) 
    {
        State = WindowState.Normal;
        Size = new Size(minWidth, minHeight);
        Location = new Point(100, 100);
    }

    [JsonIgnore]
    public bool CarsChanged { get; set; } = false;

    public Point Location { get; set; }
    public Size Size { get; set; }
    public WindowState State {  get; set; }
    
    public string PathOfCarsFolder 
    {
        get => _PathOfCarsFolder;
        set
        {
            _PathOfCarsFolder = value;
            NotifyPropertyChanged();
        }
    } 

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
