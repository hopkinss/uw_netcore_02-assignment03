using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using MahApps.Metro.IconPacks;

namespace Assignment03.Converters
{
    public class ListHeaderImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value ? PackIconRemixIconKind.ArrowDownCircleLine : PackIconRemixIconKind.ArrowUpCircleLine; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
