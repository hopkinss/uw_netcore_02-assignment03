using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace Assignment03.Converters
{
    public class PassWordStrengthColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush[] strength = {new SolidColorBrush(Colors.Red),
            new SolidColorBrush(Colors.Tomato),
            new SolidColorBrush(Colors.YellowGreen),
            new SolidColorBrush(Colors.Green)};

            return strength[(int)value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
