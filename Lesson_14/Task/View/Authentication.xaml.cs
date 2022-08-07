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

namespace Task
{
    /// <summary>
    /// Логика взаимодействия для Authentication.xaml
    /// </summary>
    public partial class Authentication : Window
    {
        public Authentication()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            if (SecondName.Text == "" || FirstName.Text == "" || MiddleName.Text == "")
            {
                MessageBox.Show("Не введены все данные");
            }
            else
            {
                DialogResult = true;
            }
        }
    }
}
