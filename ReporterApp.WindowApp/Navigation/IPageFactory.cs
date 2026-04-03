using System.Windows.Controls;

namespace ReporterApp.WindowApp.Navigation;

/// <summary>
/// Фабрика для создания страниц приложения
/// </summary>
public interface IPageFactory
{
    /// <summary>
    /// Создать страницу указанного типа
    /// </summary>
    Page CreatePage<TPage>(object? parameter = null) where TPage : Page;

    /// <summary>
    /// Создать страницу по типу
    /// </summary>
    Page CreatePage(Type pageType, object? parameter = null);
}
