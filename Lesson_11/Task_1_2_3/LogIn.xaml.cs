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

namespace Lesson_11
{

    
    public partial class LogIn : Window
    {
               
        private string _login { get { return textBoxLogin.Text; } }
        private string _password { get { return textBoxPassword.Text; } }
        public LogIn()
        {
            InitializeComponent();
        }


        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            if (!FieldsCheck())
            {
                return;
            }
            if (comboBoxEmployer.SelectedIndex == 0)
            {
                CheckPassword(new Consultant());
            }
            else
            {
               CheckPassword(new Manager());
            }
            
        }

        private void CheckPassword(Employer employer)
        {
            
                if (employer.LoginPassword.TryGetValue(_login, out string value))
                {
                    if (value == _password)
                    {
                        this.Content = employer;
                        DialogResult = true;
                    }
                    else MessageBox.Show("Неверный пароль");
                }
                else MessageBox.Show("Такого логина нет в базе");            
        }

        private bool FieldsCheck()
        {
            if (comboBoxEmployer.SelectedIndex == -1)
            {
                MessageBox.Show("Не выбран тип работника");
                return false;
            }
            if (textBoxLogin.Text == "Логин" || textBoxPassword.Text == "Пароль")
            {
                MessageBox.Show("Не введены регистрационные данные");
                return false;
            }
            return true;
        }
        private void buttonNewEmployer_Click(object sender, RoutedEventArgs e)
        {
            if (!FieldsCheck())
            {
                return;
            }
            if (comboBoxEmployer.SelectedIndex == 0)
            {
                AddNewEmployer(new Consultant());
            }
            else
            {
                AddNewEmployer(new Manager());
            }
        }

        private void AddNewEmployer(Employer employer)
        {
            if (!employer.LoginPassword.TryGetValue(_login, out string value))
            {
                employer.LoginPassword.Add(_login, _password);
                employer.SaveLoginPass();
                MessageBox.Show($"Новый пользователь \"{comboBoxEmployer.SelectionBoxItem}\" добавлен");
            }
            else MessageBox.Show("Такой логин занят");
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
