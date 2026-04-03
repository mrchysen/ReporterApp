using System.Windows.Controls;

namespace ReporterApp.WindowApp.Navigation;

/// <summary>
/// Реализация сервиса навигации
/// </summary>
public class NavigationService : INavigationService
{
    private readonly Frame _frame;
    private readonly IPageFactory _pageFactory;

    public NavigationService(Frame frame, IPageFactory pageFactory)
    {
        _frame = frame;
        _pageFactory = pageFactory;
    }

    public void NavigateTo<TPage>() where TPage : Page
        => NavigateTo<TPage>(null);

    public void NavigateTo<TPage>(object? parameter) where TPage : Page
    {
        var page = _pageFactory.CreatePage<TPage>(parameter);
        _frame.Content = page;
    }
}
