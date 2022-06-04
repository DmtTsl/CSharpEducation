using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Task_1_2_3_MVVM
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

        private void textBoxPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {            
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
                MessageBox.Show("Допустимо вводить только цифры");
            }
               
        }

        private void textBoxPassNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
                MessageBox.Show("Допустимо вводить только цифры");
            }
        }

    }
}
