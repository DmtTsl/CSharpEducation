using System;
using System.Windows.Data;

namespace Lesson_15
{
    public class PhoneNumberConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
           System.Globalization.CultureInfo culture)
        {
            string s = value as string;
            if (string.IsNullOrEmpty(s)) return null;
            if (s.Length == 10)
            {
                return "+7(" + s.Substring(0, 3) + ")" + s.Substring(3);
            }
            else return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            string s = (string)value;
            if (string.IsNullOrEmpty(s)) return null;
            if (s.Length < 11) return s;
            s = s.Remove(0, 3);
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                {
                    s = s.Replace(c.ToString(), "");
                }
            }
            return s;    
        }
    }
}
