using System.Windows.Controls;

namespace ReporterApp.WindowApp.Utils;

public class PageNavigatorService(Frame frame)
{
    public void NavigateTo(Page page) 
        => frame.Content = page;
}
