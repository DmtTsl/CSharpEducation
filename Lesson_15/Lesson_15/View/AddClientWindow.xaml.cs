using System;
using System.Windows;
using System.Windows.Input;
using ExceptionsLibrary;
using LogicLibrary;

namespace Lesson_15
{
    /// <summary>
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        public AddClientWindow()
        {
            InitializeComponent();
        }
        private void textBoxInt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try { Checks.IsInt(e); }
            catch (NotIntException ex) { ex.Message.Show(); }
        }

        private void textBoxString_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try { Checks.IsString(e); }
            catch (NotStringException ex) { ex.Message.Show(); }
        }
    }
}
