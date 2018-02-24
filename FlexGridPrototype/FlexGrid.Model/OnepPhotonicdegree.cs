using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM;

namespace FlexGrid.Model
{
    public class OnepPhotonicdegree : PropertyChangedBase
    {
        private OnepNetwork _onepNetwork;
        private OnepManagedelement _onepManagedelement = null;
        
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

        public OnepManagedelement OnepManagedelement
        {
            get
            {
                return _onepManagedelement;
            }
            set
            {
                var oldValue = _onepManagedelement;
                if (oldValue != value)
                {
                    _onepManagedelement = value;
                    RaisePropertyChanged("OnepManagedelement");
                }
            }
        }
    }
}
