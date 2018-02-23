using FirstSample.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSample.ViewModel
{
    class MainWindowViewModel : PropertyChangedBase
    {
        public MainWindowViewModel()
        {
            ButtonClickedMessage = "Click one of the buttons above to see magic !!";
        }

        private DelegateCommand _saveCommand = null;
        public DelegateCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new DelegateCommand(
                        () =>
                        {
                            ButtonClickedMessage = "Save Command Called - Bind to DelegateCommand with no command parameters";
                        });
                }
                return _saveCommand;
            }
        }

        private DelegateCommand<string> _saveAsCommand = null;
        public DelegateCommand<string> SaveAsCommand
        {
            get
            {
                if (_saveAsCommand == null)
                {
                    _saveAsCommand = new DelegateCommand<string>(
                        (s) =>
                        {
                            ButtonClickedMessage = string.Format("DelegateCommand<T> called with parameter: {0}", s);
                        });
                }
                return _saveAsCommand;
            }
        }

        private string _buttonClickedMessage;
        public string ButtonClickedMessage {
            get
            {
                return _buttonClickedMessage;
            }
            set
            {
                if (value != _buttonClickedMessage)
                {
                    _buttonClickedMessage = value;
                    NotifyPropertyChanged(); // New ! No need to pass property name - see implementation of NotifyPropertyChanged in base class
                }
            }
        }
    }
}
