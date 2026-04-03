using System.Windows.Controls;

namespace ReporterApp.WindowApp.Navigation;

/// <summary>
/// Сервис для навигации между страницами приложения
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Перейти к указанной странице
    /// </summary>
    void NavigateTo<TPage>() where TPage : Page;

    /// <summary>
    /// Перейти к указанной странице с параметром
    /// </summary>
    void NavigateTo<TPage>(object? parameter) where TPage : Page;
}
