using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace BlueOrigin.Wpf.Controls
{
    /// <summary>
    /// 带凸显状态的开关
    /// </summary>
    public class HighlightedSwitch : ToggleButton
    {
        public static DependencyProperty IsHighlightedProperty = DependencyProperty.Register("IsHighlighted", typeof(bool), typeof(HighlightedSwitch));
        public bool IsHighlighted
        {
            get
            {
                return (bool)GetValue(IsHighlightedProperty);
            }
            set
            {
                SetValue(IsHighlightedProperty, value);
            }
        }


        static HighlightedSwitch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HighlightedSwitch), new FrameworkPropertyMetadata(typeof(HighlightedSwitch)));
        }
    }
}
