using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Reporter.Logic.Configuration;

public class WindowConfiguration : INotifyPropertyChanged
{
    protected string _PathOfCarsFolder = Directory.GetCurrentDirectory() + "\\CarsFolder";

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

    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
