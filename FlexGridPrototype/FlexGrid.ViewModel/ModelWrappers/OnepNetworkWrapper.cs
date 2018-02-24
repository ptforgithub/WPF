using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FlexGrid.Model;
using Microsoft.Practices.ServiceLocation;
using MVVM;

namespace FlexGrid.ViewModel.ModelWrappers
{
    public class OnepNetworkWrapper : PropertyChangedBase
    {
        private OnepNetwork _onepNetwork; // Model instance
        private ObservableCollection<OnepSiteWrapper> _onepSiteWrappers = new ObservableCollection<OnepSiteWrapper>();
        private ObservableCollection<OnepManagedelementWrapper> _onepManagedelementWrappers = new ObservableCollection<OnepManagedelementWrapper>();
        private ObservableCollection<OnepPhotonicdegreeWrapper> _onepPhotonicdegreeWrappers = new ObservableCollection<OnepPhotonicdegreeWrapper>();

        private ICommand _commandCreateNewSite;

        Services.IUndoManager _undoManager;

        public OnepNetworkWrapper(OnepNetwork onepNetwork)
        {
            _onepNetwork = onepNetwork;
            //_undoManager = UndoManager.UndoManager.Instance;
            _undoManager = ServiceLocator.Current.GetInstance<Services.IUndoManager>();
        }

        public ObservableCollection<OnepSiteWrapper> OnepSiteWrappers
        {
            get
            {
                return _onepSiteWrappers;
            }
            set
            {
                var oldValue = _onepSiteWrappers;
                if (oldValue != value)
                {
                    _onepSiteWrappers = value;
                    RaisePropertyChanged("OnepSiteWrappers");
                }
            }
        }

        public ObservableCollection<OnepManagedelementWrapper> OnepManagedelementWrappers
        {
            get
            {
                return _onepManagedelementWrappers;
            }
            set
            {
                var oldValue = _onepManagedelementWrappers;
                if (oldValue != value)
                {
                    _onepManagedelementWrappers = value;
                    RaisePropertyChanged("OnepManagedelementWrappers");
                }
            }
        }

        public ObservableCollection<OnepPhotonicdegreeWrapper> OnepPhotonicdegreeWrappers
        {
            get
            {
                return _onepPhotonicdegreeWrappers;
            }
            set
            {
                var oldValue = _onepPhotonicdegreeWrappers;
                if (oldValue != value)
                {
                    _onepPhotonicdegreeWrappers = value;
                    RaisePropertyChanged("OnepPhotonicdegreeWrappers");
                }
            }
        }


        // TODO : Except that I'm not sure if this should be in the wrapper class
        public ICommand CommandCreateNewSite2 // Older version .. depricated
        {
            get
            {
                if (_commandCreateNewSite == null)
                {
                    _commandCreateNewSite = new DelegateCommand<object>(
                        (object parameter) =>
                        {
                            Debug.WriteLine(string.Format("CommandCreateNewSite: parameter = {0}", parameter != null ? parameter.ToString() : "null"));

                            if (parameter != null)
                            {
                                Point point = (Point)parameter;
                                // To make this point as the center point of the site, we must take ths site's Width and Height into account
                                int width = 20;
                                int height = 20;
                                int x = (int)point.X - (width / 2);
                                int y = (int)point.Y - (height / 2);
                                OnepSite onepSite = new OnepSite() { X = x, Y = y, Width = width, Height = height };
                                OnepSiteWrapper onepSiteWrapper = new OnepSiteWrapper(onepSite);
                                this.OnepSiteWrappers.Add(onepSiteWrapper);
                            }
                        }
                        );
                }
                return _commandCreateNewSite;
            }
        }

        public ICommand CommandCreateNewSite
        {
            get
            {
                if (_commandCreateNewSite == null)
                {
                    _commandCreateNewSite = new DelegateCommand<Point>(
                        (Point parameter) => 
                        {
                            //Debug.WriteLine(string.Format("CommandCreateNewSite: parameter = {0}", parameter != null ? parameter.ToString() : "null"));

                            if (parameter != null)
                            {
                                Point point = (Point)parameter;
                                // To make this point as the center point of the site, we must take ths site's Width and Height into account
                                int width = 20;
                                int height = 20;
                                int x = (int)point.X - (width / 2);
                                int y = (int)point.Y - (height / 2);

                                OnepSite onepSite = new OnepSite() { X = x, Y = y, Width = width, Height = height };
                                OnepSiteWrapper onepSiteWrapper = new OnepSiteWrapper(onepSite);

                                
                                var undoableCommand = new DelegateUndoCommand(
                                    "Create New Site",
                                    () => 
                                    {
                                        // Do / Redo
                                        this.OnepSiteWrappers.Add(onepSiteWrapper);
                                    },
                                    () => 
                                    {
                                        // Undo
                                        if (OnepSiteWrappers.Contains(onepSiteWrapper))
                                        {
                                            OnepSiteWrappers.Remove(onepSiteWrapper);
                                        }
                                    },
                                    () => 
                                    {
                                        // CanExecute
                                        return true;
                                    });
                                //undoableCommand.Execute((Point)parameter);
                                //undoableCommand.Execute();
                                _undoManager.Execute(undoableCommand);

                            }
                        },
                        (Point parameter) => 
                        {
                            return true;
                        }
                        );
                }
                return _commandCreateNewSite;
            }
        }
    }
}
