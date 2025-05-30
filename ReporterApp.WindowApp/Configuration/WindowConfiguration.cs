using System.Windows;

namespace Reporter.Logic.Configuration;

public class WindowConfiguration
{
    public WindowConfiguration() : this(1200, 1000) { }

    public WindowConfiguration(int minWidth, int minHeight) 
    {
        State = WindowState.Normal;
        Size = new Size(minWidth, minHeight);
        Location = new Point(100, 100);
        UrlOfWebToLaunch = string.Empty;
    }

    public Point Location { get; set; }
    public Size Size { get; set; }
    public WindowState State {  get; set; }

    public string UrlOfWebToLaunch { get; set; }
}
