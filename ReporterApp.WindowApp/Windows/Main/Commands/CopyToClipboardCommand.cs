using ReporterApp.WindowApp.Services;
using System.Windows;

namespace ReporterApp.WindowApp.Windows.Main.Commands;

public class CopyToClipboardCommand : BaseMainWindowCommand
{
    private readonly IReportService _reportService;

    public CopyToClipboardCommand(IReportService reportService)
    {
        _reportService = reportService;
    }

    public override bool CanExecute(object? parameter)
        => _reportService.Builder != null;

    public override void Execute(object? parameter)
    {
        Clipboard.SetText(_reportService.Builder!.GetReport());
    }
}
