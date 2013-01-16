using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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

namespace WtfMeter
{
    /// <summary>
    /// Interaction logic for GaugeControl.xaml
    /// </summary>
    public partial class GaugeControl : UserControl
    {
        public GaugeControl()
        {
            TickMarks = new ObservableCollection<TickMark>();
            InitializeComponent();
            UpdateTicks(null, null);
            var pd = DependencyPropertyDescriptor.FromProperty(MinAngleProperty, typeof(GaugeControl));
            pd.AddValueChanged(this, UpdateTicks);
            pd = DependencyPropertyDescriptor.FromProperty(MaxAngleProperty, typeof(GaugeControl));
            pd.AddValueChanged(this, UpdateTicks);
            pd = DependencyPropertyDescriptor.FromProperty(MaxProperty, typeof(GaugeControl));
            pd.AddValueChanged(this, UpdateTicks);
            pd = DependencyPropertyDescriptor.FromProperty(MinProperty, typeof(GaugeControl));
            pd.AddValueChanged(this, UpdateTicks);
            pd = DependencyPropertyDescriptor.FromProperty(NumberOfTicksProperty, typeof(GaugeControl));
            pd.AddValueChanged(this, UpdateTicks);
        }

        private void UpdateTicks(object o, EventArgs e)
        {
            double step = (Max - Min) / NumberOfTicks;
            double angleStep = (MaxAngle - MinAngle) / NumberOfTicks;
            var ticks = Enumerable.Range(0, NumberOfTicks + 1)
                          .Select(i => new TickMark { Text = (Max - i * step).ToString("N0"), Angle = (MaxAngle - i * angleStep) }).ToList();
            if (!TickMarks.SequenceEqual(ticks))
            {
                TickMarks.Clear();
                foreach (var tickMark in ticks)
                {
                    TickMarks.Add(tickMark);
                }
            }
        }

        public static readonly DependencyProperty CenterTextProperty = DependencyProperty.Register("centerText", typeof(string), typeof(GaugeControl), new PropertyMetadata("Center text"));
        [Category("Common")]
        public string CenterText
        {
            get { return (string)GetValue(CenterTextProperty); }
            set
            {
                SetValue(CenterTextProperty, value);
                centreText.Text = value;
            }
        }

        public static readonly DependencyProperty MinProperty = DependencyProperty.Register("Min", typeof(double), typeof(GaugeControl), new PropertyMetadata(0.0));
        [Category("Common")]
        public double Min
        {
            get { return (double)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register("Max", typeof(double), typeof(GaugeControl), new PropertyMetadata(100.0));
        [Category("Common")]
        public double Max
        {
            get { return (double)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        public static readonly DependencyProperty MaxAngleProperty = DependencyProperty.Register("MaxAngle", typeof(double), typeof(GaugeControl), new PropertyMetadata(160.0));
        [Category("Common")]
        public double MaxAngle
        {
            get { return (double)GetValue(MaxAngleProperty); }
            set { SetValue(MaxAngleProperty, value); }
        }

        public static readonly DependencyProperty MinAngleProperty = DependencyProperty.Register("MinAngle", typeof(double), typeof(GaugeControl), new PropertyMetadata(-160.0));
        [Category("Common")]
        public double MinAngle
        {
            get { return (double)GetValue(MinAngleProperty); }
            set { SetValue(MinAngleProperty, value); }
        }

        public static readonly DependencyProperty NumberOfTicksProperty = DependencyProperty.Register("NumberOfTicks", typeof(int), typeof(GaugeControl), new PropertyMetadata(10));
        [Category("Common")]
        public int NumberOfTicks
        {
            get { return (int)GetValue(NumberOfTicksProperty); }
            set { SetValue(NumberOfTicksProperty, value); }
        }

        public static readonly DependencyProperty TextRadiusProperty = DependencyProperty.Register("TextRadius", typeof(double), typeof(GaugeControl), new PropertyMetadata(120.0));
        [Category("Common")]
        public double TextRadius
        {
            get { return (double)GetValue(TextRadiusProperty); }
            set
            {
                SetValue(TextRadiusProperty, value);
            }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(GaugeControl), new PropertyMetadata(50.0));

        [Category("Common")]
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set
            {
                if (value < Min || value > Max)
                    return;
                SetValue(ValueProperty, value);

                double angle = MinAngle + (MaxAngle - MinAngle)*value/(Max - Min);
                ValueRotation.Angle = angle;
            }
        }

        public ObservableCollection<TickMark> TickMarks { get; set; }

    }

    public class TickMark : DependencyObject
    {
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof (double), typeof (TickMark), new PropertyMetadata(default(double)));

        public double Angle
        {
            get { return (double) GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof (string), typeof (TickMark), new PropertyMetadata("kdfg"));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }

    public class ValueToAngleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var gaugeControl = (GaugeControl)values[1];
            double angle = gaugeControl.MinAngle + (gaugeControl.MaxAngle - gaugeControl.MinAngle) * ((double)values[0]) / (gaugeControl.Max - gaugeControl.Min);
            return angle;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NegateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -1 * ((double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
