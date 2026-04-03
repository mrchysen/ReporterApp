using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace ReporterApp.WindowApp.Navigation;

/// <summary>
/// Фабрика страниц, использующая DI контейнер для создания ViewModel'ей
/// </summary>
public class PageFactory : IPageFactory
{
    private readonly IServiceProvider _serviceProvider;

    public PageFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Page CreatePage<TPage>(object? parameter = null) where TPage : Page
        => CreatePage(typeof(TPage), parameter);

    public Page CreatePage(Type pageType, object? parameter = null)
    {
        var page = (Page)_serviceProvider.GetRequiredService(pageType);
        
        if (page is IPageWithParameter pageWithParameter && parameter != null)
        {
            pageWithParameter.SetParameter(parameter);
        }

        return page;
    }
}

/// <summary>
/// Интерфейс для страниц, принимающих параметры
/// </summary>
public interface IPageWithParameter
{
    void SetParameter(object? parameter);
}
