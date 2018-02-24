using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM
{
    public class DelegateCommand : ICommand
    {
        private Action _execute;
        private Func<bool> _canExecute;


        public DelegateCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute();
            }
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (_execute != null)
            {
                _execute();
            }
        }
    }

    public class DelegateCommand<T> : ICommand
    {
        private Action<T> _execute;
        private Func<T, bool> _canExecute;

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute((T)parameter);
            }
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (_execute != null)
            {
                _execute((T)parameter);
            }
        }
    }


    // As much as I tried, I couldnt get the undo-manager to use DelegateUndoCommand<T> nicely
    // So I'll just stick to DelegateUndoCommand (without <T>)


    public class DelegateUndoCommand : ICommand
    {
        private string _commandDescription;
        private Action _do;
        private Action _undo;
        private Func<bool> _canDo;

        public DelegateUndoCommand(string commandDescription, Action @do, Action undo, Func<bool> canDo = null)
        {
            _commandDescription = commandDescription;
            _do = @do;
            _undo = undo;
            _canDo = canDo;
        }

        public bool CanExecute(object parameter)
        {
            if (_canDo == null)
                return true;

            return _canDo();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (_do != null)
            {
                Debug.WriteLine(string.Format("Do command: {0}", _commandDescription));
                _do();
            }
        }

        public void Undo()
        {
            if (_undo != null)
            {
                Debug.WriteLine(string.Format("Undo command: {0}", _commandDescription));
                _undo();
            }
        }

    }

    // Obsolete class 
    public class DelegateUndoCommand<T> : ICommand
    {
        private Action<T> _do;
        private Action _undo;
        private Func<T, bool> _canDo;

        public DelegateUndoCommand(Action<T> @do, Action undo, Func<T, bool> canDo = null)
        {
            _do = @do;
            _undo = undo;
            _canDo = canDo;
        }

        public bool CanExecute(object parameter)
        {
            if (_canDo == null)
                return true;

            return _canDo((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (_do != null)
            {
                _do((T)parameter);
            }
        }

        public void Undo()
        {
            if (_undo != null)
            {
                _undo();
            }
        }

    }

}
