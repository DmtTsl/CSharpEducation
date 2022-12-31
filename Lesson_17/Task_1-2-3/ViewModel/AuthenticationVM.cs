
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Task_1_2_3
{
    public class AuthenticationVM:INotifyPropertyChanged
    {
        private string _login;
        public string Login 
        { 
            get => _login;
            set 
            {
                _login = value;
                if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password)) ButtonState = false;
                else ButtonState = true;
            }
        }
        private string _password;
        public string Password 
        {
            get => _password;
            set 
            {
                _password = value;
                if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password)) ButtonState = false;
                else ButtonState = true;
            } 
        }

        private bool _buttonState;
        public bool ButtonState
        {
            get => _buttonState;
            set
            {
                _buttonState = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
