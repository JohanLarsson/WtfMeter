using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Wtfmeter
{
    /// <summary>
    /// Interaction logic for GaugeControl.xaml
    /// </summary>
    public partial class GaugeControl : UserControl
    {
        public GaugeControl()
        {
            CenterText = "WTF/min";
            Max = 120;
            Min = 10;
            MinAngle = 160;
            MaxAngle = -160;
            NumberOfTicks = 10;
            Value = 66;
            UpdateTicks();
            InitializeComponent();

        }

        private void UpdateTicks()
        {
            double step = (Max - Min)/NumberOfTicks;
            double angleStep = (MaxAngle - MinAngle)/NumberOfTicks;
            var ticks =
                Enumerable.Range(0, NumberOfTicks)
                          .Select(i => new TickMark {Text = (Min + i*step).ToString("N0"), Angle = (MinAngle + i*angleStep)});
            TickMarks = new ObservableCollection<TickMark>(ticks);
        }

        public static readonly DependencyProperty CenterTextProperty = DependencyProperty.Register("centerText", typeof (string), typeof (GaugeControl), new PropertyMetadata(default(string)));

        public string CenterText
        {
            get { return (string) GetValue(CenterTextProperty); }
            set { SetValue(CenterTextProperty, value); }
        }

        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register("Max", typeof (double), typeof (GaugeControl), new PropertyMetadata(default(double)));

        public double Max
        {
            get { return (double) GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        public static readonly DependencyProperty MaxAngleProperty = DependencyProperty.Register("MaxAngle", typeof (double), typeof (GaugeControl), new PropertyMetadata(default(double)));

        public double MaxAngle
        {
            get { return (double) GetValue(MaxAngleProperty); }
            set { SetValue(MaxAngleProperty, value); }
        }

        public static readonly DependencyProperty MinProperty = DependencyProperty.Register("Min", typeof(double), typeof(GaugeControl), new PropertyMetadata(default(double)));

        public double Min
        {
            get { return (double) GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        public static readonly DependencyProperty MinAngleProperty = DependencyProperty.Register("MinAngle", typeof (double), typeof (GaugeControl), new PropertyMetadata(default(double)));

        public double MinAngle
        {
            get { return (double) GetValue(MinAngleProperty); }
            set { SetValue(MinAngleProperty, value); }
        }

        public static readonly DependencyProperty NumberOfTicksProperty = DependencyProperty.Register("NumberOfTicks", typeof (int), typeof (GaugeControl), new PropertyMetadata(default(int)));

        public int NumberOfTicks
        {
            get { return (int) GetValue(NumberOfTicksProperty); }
            set { SetValue(NumberOfTicksProperty, value); }
        }

        public static readonly DependencyProperty TextRadiusProperty = DependencyProperty.Register("TextRadius", typeof (double), typeof (GaugeControl), new PropertyMetadata(default(double)));

        public double TextRadius
        {
            get { return (double) GetValue(TextRadiusProperty); }
            set { SetValue(TextRadiusProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof (double), typeof (GaugeControl), new PropertyMetadata(default(double)));

        public double Value
        {
            get { return (double) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public ObservableCollection<TickMark> TickMarks { get; set; }
    }

    public class TickMark
    {
        public double Angle { get; set; }
        public string Text { get; set; }
    }
}
