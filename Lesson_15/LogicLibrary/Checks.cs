using ExceptionsLibrary;
using System;
using System.Windows.Input;

namespace LogicLibrary
{
    public static class Checks
    {
        public  static void IsInt(TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
                throw new NotIntException("Допустимо вводить только цифры");
            }
        }
        public static void IsString(TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
                throw new NotStringException("Допустимо вводить только буквы");
            }
        }
    }
}
