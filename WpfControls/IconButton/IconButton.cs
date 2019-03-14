using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BlueOrigin.Wpf.Controls
{
    /// <summary>
    /// 图标按钮
    /// </summary>
    public class IconButton : Button
    {
        public static DependencyProperty NormalImageProperty = DependencyProperty.Register("NormalImage", typeof(ImageSource), typeof(IconButton), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange, null));

        public ImageSource NormalImage
        {
            get
            {
                return (ImageSource)GetValue(NormalImageProperty);
            }
            set
            {
                SetValue(NormalImageProperty, value);
            }
        }

        public static DependencyProperty PressedImageProperty = DependencyProperty.Register("PressedImage", typeof(ImageSource), typeof(IconButton), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange, null));

        public ImageSource PressedImage
        {
            get
            {
                return (ImageSource)GetValue(PressedImageProperty);
            }
            set
            {
                SetValue(PressedImageProperty, value);
            }
        }

        public static DependencyProperty HoverImageProperty = DependencyProperty.Register("HoverImage", typeof(ImageSource), typeof(IconButton), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange, null));

        public ImageSource HoverImage
        {
            get
            {
                return (ImageSource)GetValue(HoverImageProperty);
            }
            set
            {
                SetValue(HoverImageProperty, value);
            }
        }

        public static DependencyProperty DisabledImageProperty = DependencyProperty.Register("DisabledImage", typeof(ImageSource), typeof(IconButton), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange, null));

        public ImageSource DisabledImage
        {
            get
            {
                return (ImageSource)GetValue(DisabledImageProperty);
            }
            set
            {
                SetValue(DisabledImageProperty, value);
            }
        }


        public static DependencyProperty ImageSizeProperty = DependencyProperty.Register("ImageSize", typeof(double), typeof(IconButton));

        public double ImageSize
        {
            get
            {
                return (double)GetValue(ImageSizeProperty);
            }
            set
            {
                SetValue(ImageSizeProperty, value);
            }
        }



        static IconButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconButton), new FrameworkPropertyMetadata(typeof(IconButton)));
        }
    }
}
