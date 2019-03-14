using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace BlueOrigin.Wpf.Controls
{
    public class LeftCornerRadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is CornerRadius)
            {
                var cornerRadius = (CornerRadius)value;

                return new CornerRadius(cornerRadius.TopLeft, 0, 0, cornerRadius.BottomLeft);
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
