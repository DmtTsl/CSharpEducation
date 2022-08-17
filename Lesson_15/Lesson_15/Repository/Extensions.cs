using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lesson_15
{
    public static class Extensions
    {
        public static void Show (this string message)
        {
            MessageBox.Show(message, "ВНИМАНИЕ", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
