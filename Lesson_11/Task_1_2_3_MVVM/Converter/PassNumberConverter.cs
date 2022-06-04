using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.IO;
using System.Windows;

namespace Task_1_2_3_MVVM
{
    public class PassNumberConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            string s = value as string;
            if (string.IsNullOrEmpty(s)) return null;
            else if (s.Length == 10) return s.Insert(4, " ");
            else return s;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            string s = (string)value;
            return s.Replace(" ", "");
        }
       
    }
}
