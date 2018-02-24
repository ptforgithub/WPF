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

namespace FlexGrid.UI.Views.Canvas
{
    /// <summary>
    /// Interaction logic for FlexGridCanvas.xaml
    /// </summary>
    public partial class FlexGridCanvas : UserControl
    {
        public FlexGridCanvas()
        {
            Debug.WriteLine("FlexGridCanvas");
            InitializeComponent();
            if (DataContext != null)
            {
                Debug.WriteLine(string.Format("Canvas DataContext = {0}", DataContext.ToString()));
            }
            else
            {
                Debug.WriteLine("Canvas DataContext = null");
            }
        }
    }
}
