using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wtfmeter
{
    /// <summary>
    /// Interaction logic for GaugeWindow.xaml
    /// </summary>
    public partial class GaugeWindow : Window
    {
        public GaugeWindow()
        {
            InitializeComponent();
        }

        private void GaugeControlMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void GaugeControl_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Math.Sign(e.Delta) < 0)
                WtfMeter.Value--;
            else
                WtfMeter.Value++;
        }

    }
}

