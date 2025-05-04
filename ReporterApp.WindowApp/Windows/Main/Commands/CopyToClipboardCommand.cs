using ReporterApp.WindowApp.Utils;
using ReporterApp.WindowApp.Windows.Main.Comands;
using System.Windows;

namespace ReporterApp.WindowApp.Windows.Main.Commands;

public class CopyToClipboardCommand : BaseMainWindowCommand
{
    private ViewModelMediator _mediator;

    public CopyToClipboardCommand(ViewModelMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Execute(object? parameter)
    {
        Clipboard.SetText(_mediator.ReportPageViewModel.ReportText);
    }
}
