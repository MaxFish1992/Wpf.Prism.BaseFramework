using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BlueOrigin.Wpf.Controls
{
    /// <summary>
    /// 进度条控件
    /// </summary>
    public class ProgressBar : Slider
    {
        public static new DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(ProgressBar), new PropertyMetadata(new PropertyChangedCallback(ValuePropertyChangedCallback)));
        public new double Value
        {
            get
            {
                return (double)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }
        static void ValuePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = (ProgressBar)d;
            var newValue = (double)e.NewValue;
            sender.ValueWidth = newValue / (sender.Maximum - sender.Minimum) * sender.ActualWidth;
        }

        public static DependencyProperty Value2Property = DependencyProperty.Register("Value2", typeof(double), typeof(ProgressBar), new PropertyMetadata(new PropertyChangedCallback(Value2PropertyChangedCallback)));
        public double Value2
        {
            get
            {
                return (double)GetValue(Value2Property);
            }
            set
            {
                SetValue(Value2Property, value);
            }
        }
        static void Value2PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = (ProgressBar)d;
            var newValue = (double)e.NewValue;
            sender.Value2Width = newValue / (sender.Maximum - sender.Minimum) * sender.ActualWidth;
        }

        public static DependencyProperty ValueWidthProperty = DependencyProperty.Register("ValueWidth", typeof(double), typeof(ProgressBar));
        public double ValueWidth
        {
            get
            {
                return (double)GetValue(ValueWidthProperty);
            }
            set
            {
                SetValue(ValueWidthProperty, value);
            }
        }

        public static DependencyProperty Value2WidthProperty = DependencyProperty.Register("Value2Width", typeof(double), typeof(ProgressBar));
        public double Value2Width
        {
            get
            {
                return (double)GetValue(Value2WidthProperty);
            }
            set
            {
                SetValue(Value2WidthProperty, value);
            }
        }


        static ProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressBar), new FrameworkPropertyMetadata(typeof(ProgressBar)));
        }

        public ProgressBar()
        {
            this.SizeChanged += ProgressBar_SizeChanged;
        }

        private void ProgressBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ValuePropertyChangedCallback(sender as ProgressBar, new DependencyPropertyChangedEventArgs(ValueProperty, Value, Value));
            Value2PropertyChangedCallback(sender as ProgressBar, new DependencyPropertyChangedEventArgs(Value2Property, Value2, Value2));
        }
    }
}
