
using System.Windows;
using System.Windows.Controls;

namespace Task_1_2_3
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
        
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
           this.DialogResult = true;
        }

        private void textBoxSecondName_Error(object sender, ValidationErrorEventArgs e)
        {
            
        }
    }
}
