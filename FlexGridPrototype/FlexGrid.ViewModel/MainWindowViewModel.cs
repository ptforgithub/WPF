using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlexGrid.Model;
using FlexGrid.ViewModel.ModelWrappers;
using Microsoft.Practices.ServiceLocation;
using MVVM;
using Services;

namespace FlexGrid.ViewModel
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        private ICommand _undoCommand;
        private ICommand _redoCommand;
        private IUndoManager _undoManager;

        private ICommand _closeNetworkCommand;
        private ICommand _newNetowrkCommand;
        private ICommand _recordCommand;
        private ICommand _stopRecordingCommand;
        private ICommand _playRecordingCommand;

        private OnepNetworkWrapper _onepNetworkWrapper;

        // We want to remove the default c'tor, since we want injected dependencies to be auto-resolved by Unity
        // So I commented out this constructor, and then had to remove the DataContext statement from XAML, and instead resolve the MainWindowViewModel
        // in the .cs of MainWindow.xaml using ServiceLocator / Unity
        //public MainWindowViewModel()
        //{
        //    // I need a default c'tor for the XAML to work properly
        //    _undoManager = ServiceLocator.Current.GetInstance<IUndoManager>();
        //}

        public MainWindowViewModel(IUndoManager undoManager)
        {
            _undoManager = undoManager;
        }

        public OnepNetworkWrapper OnepNetworkWrapper
        {
            get
            {
                return _onepNetworkWrapper;
            }
            set
            {
                var oldValue = _onepNetworkWrapper;
                if (oldValue != value)
                {
                    _onepNetworkWrapper = value;
                    RaisePropertyChanged("OnepNetworkWrapper");
                }
            }
        }

        public ICommand UndoCommand
        {
            get
            {
                if (_undoCommand == null)
                {
                    _undoCommand = _undoManager.UndoCommand;
                }
                return _undoCommand;
            }
        }

        public ICommand ReDoCommand
        {
            get
            {
                if (_redoCommand == null)
                {
                    _redoCommand = _undoManager.ReDoCommand;
                }
                return _redoCommand;
            }
        }

        public ICommand CloseNetworkCommand
        {
            get
            {
                if (_closeNetworkCommand == null)
                {
                    _closeNetworkCommand = new DelegateCommand(
                        () =>
                        {
                            // {{ Non undoable
                            //OnepNetworkWrapper = null;
                            //_undoManager.ClearUndoStack();
                            // }} Non undoable

                            // {{ Undoable version
                            var oldOnepNetworkWrapper = OnepNetworkWrapper;

                            var undoableCommand = new DelegateUndoCommand(
                                "Close Network",
                                () =>
                                { // Do
                                    OnepNetworkWrapper = null;
                                    _undoManager.ClearUndoStack();
                                },
                                () =>
                                { // Undo
                                    OnepNetworkWrapper = oldOnepNetworkWrapper;
                                });

                            _undoManager.Execute(undoableCommand);
                            // }} Undoable version
                        },
                        () => { return _onepNetworkWrapper != null; }
                        );
                }
                return _closeNetworkCommand;
            }
        }

        public ICommand NewNetworkCommand
        {
            get
            {
                if (_newNetowrkCommand == null)
                {
                    _newNetowrkCommand = new DelegateCommand(
                        () =>
                        {
                            // {{ Undoable version
                            var oldNetworkWrapper = OnepNetworkWrapper;
                            OnepNetwork onepNetwork = new OnepNetwork();
                            var newOnepNetworkWrapper = new OnepNetworkWrapper(onepNetwork);

                            var undoableCommand = new DelegateUndoCommand(
                                "New Network",
                                () =>
                                {
                                    // Do / ReDo
                                    OnepNetworkWrapper = newOnepNetworkWrapper;
                                },
                                () =>
                                {
                                    // Undo
                                    OnepNetworkWrapper = oldNetworkWrapper;
                                }
                                );

                            // First close the current network, then create a new network
                            if (CloseNetworkCommand != null && CloseNetworkCommand.CanExecute(null))
                            {
                                CloseNetworkCommand.Execute(null);
                            }

                            // Then create a new network
                            _undoManager.Execute(undoableCommand);

                            // }} Undoable version


                            // {{ Non undoable

                            // First close the current network, then create a new network
                            //if (CloseNetworkCommand != null && CloseNetworkCommand.CanExecute(null))
                            //{
                            //    CloseNetworkCommand.Execute(null);
                            //}
                            //// Then create a new network

                            //OnepNetwork onepNetwork = new OnepNetwork();
                            //OnepNetworkWrapper = new OnepNetworkWrapper(onepNetwork);

                            // }} Non undoable


                            // And just for the heck of it .. create a site
                            // A command within a command
                            //var createSiteCommand = OnepNetworkWrapper.CommandCreateNewSite;
                            //System.Windows.Point point = new System.Windows.Point(300, 300);
                            //createSiteCommand.Execute(point);
                        });
                }
                return _newNetowrkCommand;
            }
        }


        public ICommand RecordCommand
        {
            get
            {
                if (_recordCommand == null)
                {
                    _recordCommand = new DelegateCommand(
                        () =>
                        {
                            _undoManager.Record();
                        });
                }
                return _recordCommand;
            }
        }

        public ICommand StopRecordingCommand
        {
            get
            {
                if (_stopRecordingCommand == null)
                {
                    _stopRecordingCommand = new DelegateCommand(
                        () =>
                        {
                            _undoManager.StopRecording();
                        });
                }
                return _stopRecordingCommand;
            }
        }

        public ICommand PlayRecordingCommand
        {
            get
            {
                if (_playRecordingCommand == null)
                {
                    _playRecordingCommand = new DelegateCommand(
                        () =>
                        {
                            _undoManager.PlayRecording();
                        });
                }
                return _playRecordingCommand;
            }
        }

    }
}
