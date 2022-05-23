using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Task_1_2_3_MVVM
{
    public class LogInViewModel
    {
        public LogIn LogIn { get; set; }
        public List<Employer> EmployersList { get; set; }
        public Employer SelectedEmployer { get; set; }
        public RelayCommand AddNew { get; }
        public RelayCommand Confirm { get; }

        private RelayCommand _closed;
        public RelayCommand Closed 
        {
            get
            {
                return _closed ??
                    (_closed = new RelayCommand(obj =>
                    {
                        Login = "Логин";
                        Password = "Пароль";
                    }));
            }
        }

        private string _login = "Логин";
        public string Login { get => _login; set => _login = value; }

        private string _password = "Пароль";
        public string Password { get =>_password; set => _password = value; }

        public LogInViewModel(LogIn window)
        {
            LogIn = window;
            EmployersList = new List<Employer>()
            {
                new Consultant(),
                new Manager()
            };

            AddNew = new RelayCommand(obj=>
            {
                if (!FieldsCheck())
                {
                    return;
                }
                AddNewEmployer(obj as Employer);
            });
            Confirm = new RelayCommand(obj =>
            {
                if (!FieldsCheck())
                {
                    return;
                }
                
                CheckPassword(obj as Employer, LogIn);
            });
            
        } 
        private bool FieldsCheck()
        {            
            if (SelectedEmployer == null)
            {
                ShowMessage("Не выбран тип работника");
                return false;
            }
            if (_login == "Логин" || _password == "Пароль")
            {
                ShowMessage("Не введены регистрационные данные");
                return false;
            }
            return true;
        }
        private void AddNewEmployer(Employer employer)
        {
            if (!employer.LoginPassword.TryGetValue(_login, out string value))
            {
                employer.LoginPassword.Add(_login, _password);
                employer.SaveLoginPass();
                ShowMessage($"Новый пользователь \"{SelectedEmployer.ToString()}\" добавлен");
            }
            else ShowMessage("Такой логин занят");
        }
        private void CheckPassword(Employer employer, LogIn logIn)
        {
            if (employer.LoginPassword.TryGetValue(_login, out string value))
            {
                if (value == _password)
                {                   
                    LogIn.DialogResult = true;
                }
                else ShowMessage("Неверный пароль");
            }
            else ShowMessage("Такого логина нет в базе");
        }
        public void CloseThisView()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window is LogIn)
                {
                    window.DialogResult = true;
                    break;
                }
            }
        }
        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
