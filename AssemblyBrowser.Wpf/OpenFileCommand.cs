using System;
using System.Windows.Input;

namespace AssemblyBrowser.Wpf
{
    public class OpenFileCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public OpenFileCommand(Action execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}