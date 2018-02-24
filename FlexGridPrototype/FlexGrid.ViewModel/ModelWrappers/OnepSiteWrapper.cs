using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexGrid.Model;
using MVVM;

namespace FlexGrid.ViewModel.ModelWrappers
{
    public class OnepSiteWrapper : PropertyChangedBase
    {
        private OnepSite _onepSite;
        private int _x;
        private int _y;
        private int _width = 20;
        private int _height = 20;

        public OnepSiteWrapper(OnepSite onepSite)
        {
            this._onepSite = onepSite;
            _x = _onepSite.X;
            _y = _onepSite.Y;
            _width = _onepSite.Width;
            _height = _onepSite.Height;
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
