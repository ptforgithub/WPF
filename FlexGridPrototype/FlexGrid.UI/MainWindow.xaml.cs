using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FlexGrid.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace FlexGrid.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Debug.WriteLine("MainWindow");

            InitializeComponent();

            // Creating the instance of MainWindowViewModel via the service locator is necessary so as to invoke the appropriate constructor
            // that lists down the dependencies
            // Otherwise, we kept it in XAML, which always invokes the default constructor
            var mainWindowViewModel = ServiceLocator.Current.GetInstance<MainWindowViewModel>();

            this.DataContext = mainWindowViewModel;
            Debug.WriteLine(string.Format("MainWindow.DataContext = {0}", DataContext!= null ? DataContext.ToString() : "null"));
        }
    }
}
