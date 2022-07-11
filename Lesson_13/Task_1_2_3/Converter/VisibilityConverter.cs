using System;
using System.Windows;
using System.Windows.Data;

namespace Task_1_2_3
{
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
           System.Globalization.CultureInfo culture)
        {
            bool b = (bool)value;

            if (!b) return Visibility.Hidden;
           
            else return Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
