using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlueOrigin.Wpf.Controls
{
    /// <summary>
    /// 可切换选中状态的图标按钮
    /// </summary>
    public class IconCheckButton : ToggleButton
    {
        public ImageSource UnCheckImage
        {
            get
            {
                return (ImageSource)GetValue(UnCheckImageProperty);
            }
            set
            {
                SetValue(UnCheckImageProperty, value);
            }
        }

        public static DependencyProperty UnCheckImageProperty = DependencyProperty.Register("UnCheckImage", typeof(ImageSource), typeof(IconCheckButton), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange, null));

        public ImageSource CheckImage
        {
            get
            {
                return (ImageSource)GetValue(CheckImageProperty);
            }
            set
            {
                SetValue(CheckImageProperty, value);
            }
        }

        public static DependencyProperty CheckImageProperty = DependencyProperty.Register("CheckImage", typeof(ImageSource), typeof(IconCheckButton), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange, null));


        static IconCheckButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconCheckButton), new FrameworkPropertyMetadata(typeof(IconCheckButton)));
        }
    }
}
