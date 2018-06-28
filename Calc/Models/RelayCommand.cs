using System;
using System.Windows.Input;

namespace Calc.Models
{
    public class RelayCommand : ICommand
    {
        private Action _action;
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter) => _action != null;        

        public void Execute(object parameter)
        {
            _action?.Invoke();
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private Action<object> _action;
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter) => _action != null;        

        public void Execute(object parameter)
        {
            _action?.Invoke(parameter); 
        }
    }
}
