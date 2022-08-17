using ExceptionsLibrary;
using LogicLibrary;
using System.Windows;
using System.Windows.Input;

namespace Lesson_15
{
    /// <summary>
    /// Логика взаимодействия для AddAccountWindow.xaml
    /// </summary>
    public partial class AddAccountWindow : Window
    {
        public AddAccountWindow()
        {
            InitializeComponent();
        }     
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try { Checks.IsInt(e); }
            catch (NotIntException ex) { ex.Message.Show(); }
        }
    }
}
