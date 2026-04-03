using ReporterApp.Core.Reports;

namespace ReporterApp.WindowApp.Services;

/// <summary>
/// Сервис для управления текущим построителем отчётов
/// </summary>
public interface IReportService
{
    /// <summary>
    /// Текущий построитель отчётов
    /// </summary>
    IReportBuilder? Builder { get; set; }

    /// <summary>
    /// Установить построитель отчётов
    /// </summary>
    void SetBuilder(IReportBuilder builder);
}

/// <summary>
/// Реализация сервиса управления отчётами
/// </summary>
public class ReportService : IReportService
{
    public IReportBuilder? Builder { get; set; }

    public void SetBuilder(IReportBuilder builder)
        => Builder = builder;
}
