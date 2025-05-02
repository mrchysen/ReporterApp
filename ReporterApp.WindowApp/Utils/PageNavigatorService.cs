using System.Windows.Controls;

namespace ReporterApp.WindowApp.Utils;

public class PageNavigatorService
{
    public Frame _frame;

    public PageNavigatorService(Frame frame)
    {
        _frame = frame;
    }

    public void NavigateTo(Page page)
    {
        _frame.Content = page;
    }
}
