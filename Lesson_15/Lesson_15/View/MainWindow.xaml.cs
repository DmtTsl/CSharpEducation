using System.Windows;
using System.Windows.Input;
using ExceptionsLibrary;
using LogicLibrary;

namespace Lesson_15
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
