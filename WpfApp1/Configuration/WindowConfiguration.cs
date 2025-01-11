using System.Text.Json.Serialization;
using System.Windows;

namespace Reporter.Logic.Configuration;

public class WindowConfiguration
{
    public WindowConfiguration() { }

    public WindowConfiguration(int minWidth, int minHeight) 
    {
        State = WindowState.Normal;
        Size = new Size(minWidth, minHeight);
        Location = new Point(100, 100);
    }

    public Point Location { get; set; }
    public Size Size { get; set; }
    public WindowState State {  get; set; }
}
