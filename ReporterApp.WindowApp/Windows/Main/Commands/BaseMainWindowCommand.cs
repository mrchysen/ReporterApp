using System.Windows.Input;

namespace ReporterApp.WindowApp.Windows.Main.Comands;

public abstract class BaseMainWindowCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public virtual bool CanExecute(object? parameter) => true;

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public abstract void Execute(object? parameter);
}
