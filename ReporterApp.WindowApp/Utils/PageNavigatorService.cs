using ReporterApp.WindowApp.Pages.NewDesign.StartPage;
using System.Windows.Controls;

namespace ReporterApp.WindowApp.Utils;

public class PageNavigatorService
{
    public Frame _frame;

    public PageNavigatorService(Frame frame)
    {
        _frame = frame;
    }

    public void NavigateWithDataContext(Page page)
    {
        _frame.Content = page;
    }
}
