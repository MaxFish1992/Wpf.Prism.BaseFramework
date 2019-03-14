using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BlueOrigin.Wpf.Controls
{
    public enum SpinnerType
    {
        [Description("数字1")]
        One,
        [Description("当前单位的一个")]
        CurrentOneUnit,
        [Description("自定义倍数")]
        CustomScale,
        [Description("自定义步长")]
        CustomIncrement,
    }

    /// <summary>
    /// 数字输入控件
    /// </summary>
    [TemplatePart(Name = PART_TextBox, Type = typeof(TextBox))]
    [TemplatePart(Name = PART_IncrementButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_DecrementButton, Type = typeof(Button))]
    public class NumberBox : Control
    {
        private const string PART_TextBox = "PART_TextBox";
        private const string PART_IncrementButton = "PART_IncrementButton";
        private const string PART_DecrementButton = "PART_DecrementButton";


        private Dictionary<string, ulong> baseOrders = new Dictionary<string, ulong>() {
            { "K", 1000 }, { "M", 1000000 }, { "G", 1000000000 } , { "T", 1000000000000 } , { "P", 1000000000000000 }  };

        private Dictionary<string, ulong> orders = new Dictionary<string, ulong>() {
            { "K", 1000 }, { "M", 1000000 }, { "G", 1000000000 } , { "T", 1000000000000 } , { "P", 1000000000000000 }  };


        private TextBox _tbxNumber;// 数据输入框部分


        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(NumberBox),
            new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(ValuePropertyChangedCallback)));
        static void ValuePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var oldValue = (double)e.OldValue;
            var newValue = (double)e.NewValue;
            var control = (NumberBox)sender;

            if (control._tbxNumber != null)
            {
                control.ShowNumber(oldValue, newValue);
            }
        }
        /// <summary>
        /// 处理的数字
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty DecimalDigitsProperty = DependencyProperty.Register("DecimalDigits", typeof(int), typeof(NumberBox), new PropertyMetadata(6));
        /// <summary>
        /// 最多显示的小数位数
        /// </summary>
        public int DecimalDigits
        {
            get { return (int)GetValue(DecimalDigitsProperty); }
            set { SetValue(DecimalDigitsProperty, value); }
        }

        public static readonly DependencyProperty BasicUnitProperty = DependencyProperty.Register("BasicUnit", typeof(string), typeof(NumberBox),
            new PropertyMetadata(new PropertyChangedCallback(BasicUnitPropertyChangedCallback)));
        static void BasicUnitPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var value = (string)e.NewValue;
            var control = (NumberBox)sender;

            control.orders.Clear();

            foreach (var item in control.baseOrders)
            {
                control.orders.Add(item.Key, item.Value);
            }

            //control.orders.Add("K" + value, 1000);
            //control.orders.Add("M" + value, 1000000);
            //control.orders.Add("G" + value, 1000000000);
            //control.orders.Add("T" + value, 1000000000000);
            //control.orders.Add("P" + value, 1000000000000000);

            var length = value.Length;

            for (var index = 1; index <= length; index++)
            {
                var suffix = value.Substring(0, index);

                control.orders.Add("K" + suffix, 1000);
                control.orders.Add("M" + suffix, 1000000);
                control.orders.Add("G" + suffix, 1000000000);
                control.orders.Add("T" + suffix, 1000000000000);
                control.orders.Add("P" + suffix, 1000000000000000);
            }

            control.orders.Add(value, 1);
        }
        /// <summary>
        /// 基本单位，进制采用K,M,G,T,P
        /// </summary>
        public string BasicUnit
        {
            get { return (string)GetValue(BasicUnitProperty); }
            set { SetValue(BasicUnitProperty, value); }
        }

        public static readonly DependencyProperty IsUnitFormatEnabledProperty = DependencyProperty.Register("IsUnitFormatEnabled", typeof(bool), typeof(NumberBox), new PropertyMetadata(true));
        /// <summary>
        /// 获取或设置用单位格式化数值是否可用
        /// </summary>
        public bool IsUnitFormatEnabled
        {
            get { return (bool)GetValue(IsUnitFormatEnabledProperty); }
            set { SetValue(IsUnitFormatEnabledProperty, value); }
        }

        public static readonly DependencyProperty HasIntervalPrecedingUnitProperty = DependencyProperty.Register("HasIntervalPrecedingUnit", typeof(bool), typeof(NumberBox), new PropertyMetadata(true));
        /// <summary>
        /// 获取或设置在单位前是否显示一个空白
        /// </summary>
        public bool HasIntervalPrecedingUnit
        {
            get { return (bool)GetValue(HasIntervalPrecedingUnitProperty); }
            set { SetValue(HasIntervalPrecedingUnitProperty, value); }
        }

        public static readonly DependencyProperty AllowSpinnerSpinProperty = DependencyProperty.Register("AllowSpinnerSpin", typeof(bool), typeof(NumberBox), new PropertyMetadata(true));
        /// <summary>
        /// 启用/禁用微调按钮，在默认情况下就是右侧上下按钮
        /// </summary>
        public bool AllowSpinnerSpin
        {
            get { return (bool)GetValue(AllowSpinnerSpinProperty); }
            set { SetValue(AllowSpinnerSpinProperty, value); }
        }

        public static readonly DependencyProperty MouseWheelSpinnerActiveProperty = DependencyProperty.Register("MouseWheelSpinnerActive", typeof(bool), typeof(NumberBox), new PropertyMetadata(true));
        /// <summary>
        /// 启用/禁用鼠标滚动微调
        /// </summary>
        public bool MouseWheelSpinnerActive
        {
            get { return (bool)GetValue(MouseWheelSpinnerActiveProperty); }
            set { SetValue(MouseWheelSpinnerActiveProperty, value); }
        }

        public static readonly DependencyProperty SpinnerMoudleProperty = DependencyProperty.Register("SpinnerMoudle", typeof(SpinnerType), typeof(NumberBox), new PropertyMetadata(SpinnerType.One));
        /// <summary>
        /// 微调模式
        /// </summary>
        public SpinnerType SpinnerMoudle
        {
            get { return (SpinnerType)GetValue(SpinnerMoudleProperty); }
            set { SetValue(SpinnerMoudleProperty, value); }
        }

        public static readonly DependencyProperty IncrementProperty = DependencyProperty.Register("Increment", typeof(double), typeof(NumberBox));
        /// <summary>
        /// 微调的增量，在微调模式CustomIncrement下可用
        /// </summary>
        public double Increment
        {
            get { return (double)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register("Scale", typeof(double), typeof(NumberBox));
        /// <summary>
        /// 微调缩放的比例，在微调模式CustomScale下可用
        /// </summary>
        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        public static readonly new DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(NumberBox));
        public new Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static readonly DependencyProperty InputBackgroundProperty = DependencyProperty.Register("InputBackground", typeof(Brush), typeof(NumberBox));
        /// <summary>
        /// 输入区域背景颜色
        /// </summary>
        public Brush InputBackground
        {
            get { return (Brush)GetValue(InputBackgroundProperty); }
            set { SetValue(InputBackgroundProperty, value); }
        }

        public static readonly DependencyProperty SpinnerSpinBackgroundProperty = DependencyProperty.Register("SpinnerSpinBackground", typeof(Brush), typeof(NumberBox));
        /// <summary>
        /// 微调区域的背景颜色
        /// </summary>
        public Brush SpinnerSpinBackground
        {
            get { return (Brush)GetValue(SpinnerSpinBackgroundProperty); }
            set { SetValue(SpinnerSpinBackgroundProperty, value); }
        }

        public static readonly DependencyProperty IsFocusProperty = DependencyProperty.Register("IsFocus", typeof(bool), typeof(NumberBox));
        public bool IsFocus
        {
            get { return (bool)GetValue(IsFocusProperty); }
            set { SetValue(IsFocusProperty, value); }
        }


        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NumberBox));
        /// <summary>
        /// 圆角尺寸
        /// </summary>
        public CornerRadius CornerRadius { get; set; }


        static NumberBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumberBox), new FrameworkPropertyMetadata(typeof(NumberBox)));
        }

        public NumberBox()
        {
            this.MouseWheel += Self_MouseWheel;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _tbxNumber = GetTemplateChild(PART_TextBox) as TextBox;
            _tbxNumber.KeyDown += _tbxNumber_KeyDown;
            _tbxNumber.PreviewMouseDown += _tbxNumber_PreviewMouseDown;
            _tbxNumber.GotFocus += _tbxNumber_GotFocus;
            _tbxNumber.LostFocus += _tbxNumber_LostFocus;

            ShowNumber(0, Value);// 初始化界面值

            var btnUp = GetTemplateChild(PART_IncrementButton) as Button;
            if (btnUp != null)
            {
                btnUp.Click += Button_up_Click;
            }

            var btnDown = GetTemplateChild(PART_DecrementButton) as Button;
            if (btnDown != null)
            {
                btnDown.Click += Button_down_Click;
            }
        }

        private void _tbxNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            DoChanged();
        }

        private void _tbxNumber_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _tbxNumber.Focus();
            e.Handled = true;
        }

        private void _tbxNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            IsFocus = true;
            _tbxNumber.SelectAll();
            _tbxNumber.PreviewMouseDown -= _tbxNumber_PreviewMouseDown;
        }

        private void _tbxNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            _tbxNumber.PreviewMouseDown += _tbxNumber_PreviewMouseDown;
            IsFocus = false;
            DoChanged();
        }


        private void ShowNumber(double oldValue, double newValue)
        {
            if (!IsUnitFormatEnabled)
            {
                _tbxNumber.Text = newValue.ToString();
                _tbxNumber.SelectionStart = _tbxNumber.Text.Length;
                return;
            }

            var displayValue = 0d;
            var decimalDigits = 0;
            var unit = BasicUnit;

            if (newValue < 1e3 && newValue > -1e3)
            {
                displayValue = newValue;
                decimalDigits = 0;
            }
            else if (newValue < 1e6 && newValue > -1e6)
            {
                displayValue = newValue / 1e3;

                decimalDigits = 3;
                unit = "K" + BasicUnit;
            }
            else if (newValue < 1e9 && newValue > -1e9)
            {
                displayValue = newValue / 1e6;
                decimalDigits = 6;
                unit = "M" + BasicUnit;
            }
            else if (newValue < 1e12 && newValue > -1e12)
            {
                displayValue = newValue / 1e9;
                decimalDigits = 9;
                unit = "G" + BasicUnit;
            }
            else if (newValue < 1e15 && newValue > -1e15)
            {
                displayValue = newValue / 1e12;
                decimalDigits = 12;
                unit = "T" + BasicUnit;
            }
            else if (newValue < 1e18 && newValue > -1e18)
            {
                displayValue = newValue / 1e15;
                decimalDigits = 15;
                unit = "P" + BasicUnit;
            }
            else
            {
                Value = oldValue;// 超出范围用旧值重新覆盖
            }

            if (decimalDigits > DecimalDigits)
            {
                decimalDigits = DecimalDigits;
            }

            if (string.IsNullOrWhiteSpace(unit))
            {
                _tbxNumber.Text = displayValue.ToString();
            }
            else
            {
                if (HasIntervalPrecedingUnit)
                {
                    _tbxNumber.Text = string.Format("{0:F" + decimalDigits + "} {1}", displayValue, unit);
                }
                else
                {
                    _tbxNumber.Text = string.Format("{0:F" + decimalDigits + "}{1}", displayValue, unit);
                }
            }

            _tbxNumber.SelectionStart = _tbxNumber.Text.Length;
        }


        private void DoChanged()
        {
            var text = _tbxNumber.Text;
            // 为空时
            if (string.IsNullOrEmpty(text))
            {
                Value = 0;
                return;
            }

            if (!text.EndsWith(" "))
            {
                // 为整数时
                double newNumber;
                if (double.TryParse(text, out newNumber))
                {
                    var oldNumber = Value;
                    if (oldNumber == newNumber)
                    {
                        ShowNumber(0, Value);
                    }
                    else
                    {
                        Value = newNumber;
                    }
                    return;
                }

                // 有后缀时
                foreach (var item in orders.Keys)
                {
                    if (text.ToLower().EndsWith(item.ToLower()))
                    {
                        decimal numberD;
                        var numText = text.Substring(0, text.Length - item.Length);
                        if (decimal.TryParse(numText, out numberD))
                        {
                            var order = orders[item];
                            newNumber = (double)(numberD * order);

                            var oldNumber = Value;
                            if (oldNumber == newNumber)
                            {
                                ShowNumber(0, Value);
                            }
                            else
                            {
                                Value = newNumber;
                            }
                            return;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }

            // 输入错误字符时
            var temp = Value;
            Value = Value + 1;
            Value = temp;
            //var oldStart = _tbxNumber.SelectionStart;
            //_tbxNumber.Text = Number.ToString();
            //_tbxNumber.SelectionStart = oldStart > 0 ? oldStart - 1 : 1;
        }


        private void Button_down_Click(object sender, RoutedEventArgs e)
        {
            switch (SpinnerMoudle)
            {
                case SpinnerType.One:
                    Value = Value - 1;
                    break;
                case SpinnerType.CurrentOneUnit:
                    Value = Value - GetFormatOneUnit(Value);
                    break;
                case SpinnerType.CustomScale:
                    Value = Value - Value * Scale;
                    break;
                case SpinnerType.CustomIncrement:
                    Value = Value - Increment;
                    break;
            }
        }

        private void Button_up_Click(object sender, RoutedEventArgs e)
        {
            switch (SpinnerMoudle)
            {
                case SpinnerType.One:
                    Value = Value + 1;
                    break;
                case SpinnerType.CurrentOneUnit:
                    Value = Value + GetFormatOneUnit(Value);
                    break;
                case SpinnerType.CustomScale:
                    Value = Value + Value * Scale;
                    break;
                case SpinnerType.CustomIncrement:
                    Value = Value + Increment;
                    break;
            }
        }

        // 当鼠标在空间上滚动时
        private void Self_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!MouseWheelSpinnerActive) return;

            var scale = e.Delta / 120;

            switch (SpinnerMoudle)
            {
                case SpinnerType.One:
                    Value = Value + scale * 1;
                    break;
                case SpinnerType.CurrentOneUnit:
                    Value = Value + scale * GetFormatOneUnit(Value);
                    break;
                case SpinnerType.CustomScale:
                    Value = Value + scale * Value * Scale;
                    break;
                case SpinnerType.CustomIncrement:
                    Value = Value + scale * Increment;
                    break;
            }
        }


        private double GetFormatOneUnit(double newValue)
        {
            double retValue = 1;

            if (newValue < 1e3 && newValue > -1e3)
            {
                retValue = 1;
            }
            else if (newValue < 1e6 && newValue > -1e6)
            {
                retValue = 1e3;
            }
            else if (newValue < 1e9 && newValue > -1e9)
            {
                retValue = 1e6;
            }
            else if (newValue < 1e12 && newValue > -1e12)
            {
                retValue = 1e9;
            }
            else if (newValue < 1e15 && newValue > -1e15)
            {
                retValue = 1e12;
            }
            else if (newValue < 1e18 && newValue > -1e18)
            {
                retValue = 1e15;
            }
            else
            {
                retValue = 1;
            }

            return retValue;
        }
    }
}
