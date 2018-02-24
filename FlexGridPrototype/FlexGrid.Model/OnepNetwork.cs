using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM;

namespace FlexGrid.Model
{
    public class OnepNetwork : PropertyChangedBase
    {
        private ObservableCollection<OnepSite> _onepSites = new ObservableCollection<OnepSite>();
        private ObservableCollection<OnepManagedelement> _onepManagedelements = new ObservableCollection<OnepManagedelement>();
        private ObservableCollection<OnepPhotonicdegree> _onepPhotonicdegrees = new ObservableCollection<OnepPhotonicdegree>();
    }
}
