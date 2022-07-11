using System.Windows;

namespace Task_1_2_3
{
    public class AddClientWindowVM
    {
        public class NewClientInfo
        {
            public string SecondName { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string PassportNumber { get; set; }
            public string PhoneNumber { get; set; }
        }
        public NewClientInfo NewClient { get; set; }
        AddClientWindow AddClientWindow { get; set; }  
        public AddClientWindowVM (AddClientWindow addClientWindow)
        {
            NewClient = new NewClientInfo();
            AddClientWindow = addClientWindow;
        }
        private RelayCommand _addClientCommand;
        public RelayCommand AddClientCommand
        {
            get
            {
                return _addClientCommand ??
                    (_addClientCommand = new RelayCommand(obj =>
                    {
                        if (NewClient.SecondName == "" || NewClient.FirstName == "" || NewClient.PassportNumber == "")
                        {
                            MessageBox.Show("Не введены все данные клиента. Фамилия, имя и номер паспорта обязательны для введения");
                        }
                        else if (!string.IsNullOrEmpty(NewClient.PassportNumber) && NewClient.PassportNumber.Length < 10)
                        {
                            MessageBox.Show("Номер паспорта должен состоять из 10 цифр");
                        }
                        else if (!string.IsNullOrEmpty(NewClient.PhoneNumber) && NewClient.PhoneNumber.Length < 10)
                        {
                            MessageBox.Show("Номер nелефона должен состоять из 10 цифр");
                        }
                        else
                        {
                            AddClientWindow.DialogResult = true;
                        }
                    }));
            }
        }
    }
}
