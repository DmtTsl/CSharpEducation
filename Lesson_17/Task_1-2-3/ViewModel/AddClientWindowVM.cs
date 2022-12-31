using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Task_1_2_3
{
    public class AddClientWindowVM : IDataErrorInfo, INotifyPropertyChanged
    {
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        private bool _isActionAllowed = false;
        public bool IsActionAllowed 
        {
            get=>_isActionAllowed;
            set 
            {
                _isActionAllowed = value;
                OnPropertyChanged();
            } 
        }

        public string Error => string.Empty;

        public string this[string columnName]
        {
            get 
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "SecondName":
                        if (!string.IsNullOrEmpty(SecondName))
                        {
                            foreach (char c in SecondName)
                            {
                                if (char.IsPunctuation(c) || SecondName.Length > 20)
                                {
                                    error = "Неверный формат ввода";
                                }
                            }
                        }
                        else error = "Поле не должно быть пустым";
                        break;
                    case "FirstName":
                        if (!string.IsNullOrEmpty(FirstName))
                        {
                            foreach (char c in FirstName)
                            {
                                if (char.IsPunctuation(c) || FirstName.Length > 20)
                                {
                                    error = "Неверный формат ввода";
                                }
                            }
                        }
                        else error = "Поле не должно быть пустым";
                        break;
                    case "MiddleName":
                        if (!string.IsNullOrEmpty(MiddleName))
                        {
                            foreach (char c in MiddleName)
                            {
                                if (char.IsPunctuation(c) || MiddleName.Length > 20)
                                {
                                    error = "Неверный формат ввода";
                                }
                            }
                        }
                        break;
                    case "PhoneNumber":
                        if (!string.IsNullOrEmpty(PhoneNumber))
                        {
                            foreach (char c in PhoneNumber)
                            {
                                if (!char.IsDigit(c))
                                {
                                    error = "Неверный формат ввода";
                                }
                            }
                        }                            
                        break;
                    case "Email":
                        if (!string.IsNullOrEmpty(Email))
                        {
                            foreach (char c in Email)
                            {
                                if (!Email.Contains("@") || !Email.Contains("."))
                                {
                                    error = "Неверный формат ввода";
                                }
                            }
                        }
                        else error = "Поле не должно быть пустым";
                        break;
                }
                if (string.IsNullOrEmpty(error)) IsActionAllowed = true;
                return error;
            } 
        }

        public AddClientWindowVM() { }
        public AddClientWindowVM(string secondName, string firstName, string middleName, string phoneNumber, string email)
        {
            SecondName = secondName;
            FirstName = firstName;
            MiddleName = middleName;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
