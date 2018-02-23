using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FirstSample.MVVM
{
    class DelegateCommand : ICommand
    {
        private Action _action;
        private Func<bool> _canExecute;
        public DelegateCommand(Action action, Func<bool> canExecute = null)
        {
            this._action = action;
                this._canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute();
            }
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
    class DelegateCommand<T> : ICommand
    {
        private Action<T> _action;
        private Func<T, bool> _canExecute;
        public DelegateCommand(Action<T> action, Func<T, bool> canExecute = null)
        {
            this._action = action;
            this._canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute((T)parameter);
            }
            return true;
        }

        public void Execute(object parameter)
        {
            _action((T)parameter);
        }
    }
}
