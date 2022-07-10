﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Task_1_2_3
{
    public class AccountNumberConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            string s = value.ToString().PadLeft(7, '0');

            return s;

        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            string s = (string)value;
            int i = Int32.Parse(s);
            return i;
        }
    }
}
