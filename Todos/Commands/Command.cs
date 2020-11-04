using System;
using System.Windows.Input;

namespace Todos.Commands
{
    public class Command : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public Command(Action execute) => _execute = execute;

        public Command(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _execute = executeMethod;
            _canExecute = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object parameter) => _execute?.Invoke();

        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
    //public class Command<T> : ICommand where T: class
    //{
    //    private readonly Action<T> _execute;
    //    private readonly Func<bool> _canExecute;

    //    public Command(Action<T> execute) => _execute = execute;

    //    public Command(Action<T> executeMethod, Func<bool> canExecuteMethod)
    //    {
    //        _execute = executeMethod;
    //        _canExecute = canExecuteMethod;
    //    }

    //    public event EventHandler CanExecuteChanged;

    //    public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

    //    public void Execute(object parameter) => _execute?.Invoke(parameter as T);

    //    public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    //}
}