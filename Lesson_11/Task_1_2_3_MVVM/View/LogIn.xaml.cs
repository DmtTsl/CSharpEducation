using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Task_1_2_3_MVVM
{
    /// <summary>
    /// Логика взаимодействия для LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        public LogIn()
        {
            InitializeComponent();
            
        }


        private void textBoxPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxPassword.Style == (Style)TryFindResource("Tip"))
            {
                textBoxPassword.Text = "";
                textBoxPassword.Style = (Style)TryFindResource("Normal");
            }
        }

        private void textBoxPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxPassword.Text == "")
            {
                textBoxPassword.Text = "Пароль";
                textBoxPassword.Style = (Style)TryFindResource("Tip");
            }
        }

        private void textBoxLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxLogin.Style == (Style)TryFindResource("Tip"))
            {
                textBoxLogin.Text = "";
                textBoxLogin.Style = (Style)TryFindResource("Normal");
            }
        }

        private void textBoxLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxLogin.Text == "")
            {
                textBoxLogin.Text = "Логин";
                textBoxLogin.Style = (Style)TryFindResource("Tip");
            }
        }

       
    }
}
