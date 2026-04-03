using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace ReporterApp.WindowApp.Services;

/// <summary>
/// Сервис для управления настройками отчётов (дата и т.д.)
/// </summary>
public interface IFileManagementService : INotifyPropertyChanged
{
    /// <summary>
    /// Дата отчёта
    /// </summary>
    DateTime ReportDate { get; set; }
}

/// <summary>
/// Реализация сервиса управления настройками отчётов
/// </summary>
public partial class FileManagementService : ObservableObject, IFileManagementService
{
    [ObservableProperty]
    private DateTime _reportDate = DateTime.Now.AddDays(-1);
}
