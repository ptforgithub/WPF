using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM;

namespace FlexGrid.Model
{
    public class OnepSite : PropertyChangedBase
    {
        private OnepNetwork _onepNetwork;
        private ObservableCollection<OnepManagedelement> _onepManagedelements = new ObservableCollection<OnepManagedelement>();
        private int _x = 100;
        private int _y = 100;
        private int _width = 20;
        private int _height = 20;

        public OnepNetwork OnepNetwork
        {
            get
            {
                return _onepNetwork;
            }
            set
            {
                var oldValue = _onepNetwork;
                if (oldValue != value)
                {
                    _onepNetwork = value;
                    RaisePropertyChanged("OnepNetwork");
                }
            }
        }


        public ObservableCollection<OnepManagedelement> OnepManagedelements
        {
            get
            {
                return _onepManagedelements;
            }
            set
            {
                var oldValue = _onepManagedelements;
                if (oldValue != value) // Query: Why does OnePlanner classes not use the equality check .. [?]
                {
                    _onepManagedelements = value;
                    RaisePropertyChanged("OnepManagedelements");
                }
            }
        }

        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                var oldValue = _x;
                if (oldValue != value)
                {
                    _x = value;
                    RaisePropertyChanged("X");
                }
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                var oldValue = _y;
                if (oldValue != value)
                {
                    _y = value;
                    RaisePropertyChanged("Y");
                }
            }
        }

        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                var oldValue = _width;
                if (oldValue != value)
                {
                    _width = value;
                    RaisePropertyChanged("Width");
                }
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                var oldValue = _height;
                if (oldValue != value)
                {
                    _height = value;
                    RaisePropertyChanged("Height");
                }
            }
        }
    }
}
