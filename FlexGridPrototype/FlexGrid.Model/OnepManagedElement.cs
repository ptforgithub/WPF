using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM;

namespace FlexGrid.Model
{
    public class OnepManagedelement : PropertyChangedBase
    {
        private OnepNetwork _onepNetwork;
        private OnepSite _onepSite;
        private ObservableCollection<OnepPhotonicdegree> _onepPhotonicdegrees = new ObservableCollection<OnepPhotonicdegree>();
        
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

        public OnepSite OnepSite
        {
            get
            {
                return _onepSite;
            }
            set
            {
                var oldValue = _onepSite;
                if (oldValue != value)
                {
                    _onepSite = value;
                    RaisePropertyChanged("OnepSite");
                }
            }
        }

        public ObservableCollection<OnepPhotonicdegree> OnepPhotonicdegrees
        {
            get
            {
                return _onepPhotonicdegrees;
            }
            set
            {
                var oldValue = _onepPhotonicdegrees;
                if (oldValue != value)
                {
                    _onepPhotonicdegrees = value;
                    RaisePropertyChanged("OnepPhotonicdegrees");
                }
            }
        }

    }
}
