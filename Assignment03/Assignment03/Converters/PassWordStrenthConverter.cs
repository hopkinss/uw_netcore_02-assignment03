using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using MahApps.Metro.IconPacks;

namespace Assignment03.Converters
{
    public class PassWordStrenthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PackIconRemixIconKind[] strength = { PackIconRemixIconKind.EmotionSadLine,
            PackIconRemixIconKind.EmotionUnhappyLine,
            PackIconRemixIconKind.EmotionNormalLine,
            PackIconRemixIconKind.EmotionHappyLine};

            return strength[(int)value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
